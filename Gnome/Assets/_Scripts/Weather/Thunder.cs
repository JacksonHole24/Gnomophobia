using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Thunder : MonoBehaviour
{
    public Light directionalLight;
    public AudioClip[] thunderSounds;
    public Material skyboxMaterial; // Reference to the skybox material

    public float minIntensity = 0.5f;
    public float maxIntensity = 3f;
    public float intensityIncreaseSpeed = 5f;
    public float intensityDecreaseSpeed = 2f;
    public float minFlickerIntensity = 0.2f;
    public float maxFlickerIntensity = 0.5f;
    public float flickerSpeed = 10f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 5f;
    public int simultaneousStrikes = 3;

    private AudioSource audioSource;
    private bool isFlashing = false;
    private float targetIntensity;
    private float currentIntensity;
    private float targetExposure; // New variable for lerping exposure
    private float currentExposure; // Current exposure value
    private float exposureLerpDuration = 0.2f; // Duration of lerping the exposure

    private void Awake()
    {
        directionalLight = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
        if (RenderSettings.skybox != null)
        {
            skyboxMaterial = RenderSettings.skybox;
        }
    }

    private void Start()
    {
        StartCoroutine(FlashLightning());
    }

    private IEnumerator FlashLightning()
    {
        while (true)
        {
            for (int i = 0; i < simultaneousStrikes; i++)
            {
                float waitTime = Random.Range(minWaitTime, maxWaitTime);

                yield return new WaitForSeconds(waitTime);

                isFlashing = true;
                targetIntensity = Random.Range(minIntensity, maxIntensity);
                targetExposure = 8f; // Set the target exposure to maximum value
                PlayThunderSound();

                // Increase intensity and exposure simultaneously
                while (currentIntensity < targetIntensity)
                {
                    currentIntensity += intensityIncreaseSpeed * Time.deltaTime;
                    directionalLight.intensity = currentIntensity;

                    float flickerIntensity = Mathf.Lerp(maxFlickerIntensity, minFlickerIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1f));
                    directionalLight.intensity += flickerIntensity;

                    float t = Mathf.Clamp01((currentIntensity - minIntensity) / (targetIntensity - minIntensity));
                    currentExposure = Mathf.Lerp(1.8f, targetExposure, t); // Lerping the exposure value
                    skyboxMaterial.SetFloat("_Exposure", currentExposure); // Update the exposure value

                    yield return null;
                }

                yield return new WaitForSeconds(exposureLerpDuration);

                // Decrease intensity and exposure simultaneously
                while (currentIntensity > minIntensity)
                {
                    currentIntensity -= intensityDecreaseSpeed * Time.deltaTime;
                    directionalLight.intensity = currentIntensity;

                    float t = Mathf.Clamp01((currentIntensity - targetIntensity) / (minIntensity - targetIntensity));
                    currentExposure = Mathf.Lerp(targetExposure, 1.8f, t); // Lerping back to the minimum exposure value
                    skyboxMaterial.SetFloat("_Exposure", currentExposure); // Update the exposure value

                    yield return null;
                }

                isFlashing = false;
            }
        }
    }

    private void PlayThunderSound()
    {
        if (audioSource != null && thunderSounds != null && thunderSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, thunderSounds.Length);
            AudioClip randomThunderSound = thunderSounds[randomIndex];
            audioSource.PlayOneShot(randomThunderSound);
        }
    }
}
