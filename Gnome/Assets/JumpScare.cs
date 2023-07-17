using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    [SerializeField] GameObject jumpScare;
    [SerializeField] AudioSource sound;

    private bool start;

    private float timeToPlay = 1f;
    private float timer;

    [ContextMenu("Scare")]
    public void StartScare()
    {
        start = true;
        jumpScare.SetActive(true);
        sound.Play();
    }

    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;
            if(timer > timeToPlay)
            {
                start = false;
                jumpScare.SetActive(false);
                timer = 0;
            }
        }
    }
}
