using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] float m_fireRate = 1; // Shots per second
    float m_cooldownTimer;

    [SerializeField] int m_maxAmmoAmount = 15;
    int m_ammoAmount;

    [SerializeField] bool m_showLazerSight;
    GameObject m_lazerPoint;

    Animator m_animator;

    [SerializeField] TextMeshPro m_ammoDisplayTMP;
    [SerializeField] Transform m_bulletSpawnPoint;
    [SerializeField] ParticleSystem m_shootingParticle, m_impactParticle;
    [SerializeField] TrailRenderer m_bulletTrail;

    [SerializeField] LayerMask m_layerMask;

    [SerializeField] InputActionReference m_shootInputAction;
    [SerializeField] InputActionReference m_reloadInputAction;

    Player m_player;

    public bool OnCooldown() { return m_cooldownTimer < 1; }

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_ammoAmount = m_maxAmmoAmount;

        m_player = FindObjectOfType<Player>();

        m_shootInputAction.action.performed += Shoot;
        m_reloadInputAction.action.performed += Reload;
    }

    private void Update()
    {
        if (m_cooldownTimer < 1)
        {
            m_cooldownTimer += Time.deltaTime * m_fireRate;
        }

        if (m_showLazerSight) DisplayLazerSight();
    }

    void DisplayLazerSight()
    {
        if (m_lazerPoint) Destroy(m_lazerPoint);
        RaycastHit hit;
        if (Physics.Raycast(m_bulletSpawnPoint.position, m_bulletSpawnPoint.forward, out hit, float.MaxValue, m_layerMask))
        {
            m_lazerPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            m_lazerPoint.GetComponent<MeshRenderer>().material.color = Color.red;
            m_lazerPoint.transform.position = hit.point;
            m_lazerPoint.layer = 1 << 2;
        }
    }

    void DisplayAmmo()
    {
        m_ammoDisplayTMP.text = m_ammoAmount.ToString() + "/" + m_maxAmmoAmount.ToString();
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (m_player.GetAmmoBoxes() > 0)
        {
            m_player.RemoveAmmo(1);
            m_ammoAmount = m_maxAmmoAmount;
            DisplayAmmo();
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (!OnCooldown())
        {
            if (m_ammoAmount > 0)
            {
                m_cooldownTimer = 0;

                m_ammoAmount--;
                DisplayAmmo();

                m_animator.SetBool("IsShooting", true);
                m_shootingParticle.Play();

                RaycastHit hit;
                if (Physics.Raycast(m_bulletSpawnPoint.position, m_bulletSpawnPoint.forward, out hit, float.MaxValue, m_layerMask))
                {
                    Debug.Log("Object hit: " + hit.transform);
                    TrailRenderer trail = Instantiate(m_bulletTrail, m_bulletSpawnPoint.position, Quaternion.identity);

                    StartCoroutine(SpawnTrail(trail, hit));

                    if (hit.transform?.GetComponent<GnomeObject>())
                    {


                    }
                }
                else
                {
                    TrailRenderer trail = Instantiate(m_bulletTrail, m_bulletSpawnPoint.position, Quaternion.identity);

                    StartCoroutine(SpawnTrail(trail, m_bulletSpawnPoint.position + (m_bulletSpawnPoint.forward * 100)));
                }
            }
            
        }
    }

    IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }

        m_animator.SetBool("IsShooting", false);
        Trail.transform.position = Hit.point;
        Instantiate(m_impactParticle, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }

    IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 Point)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }

        m_animator.SetBool("IsShooting", false);
        Trail.transform.position = Point;

        Destroy(Trail.gameObject, Trail.time);
    }
}
