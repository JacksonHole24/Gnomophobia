using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] float m_fireRate = 1; // Shots per second
    float m_cooldownTimer;

    Animator m_animator;

    [SerializeField] Transform m_bulletSpawnPoint;
    [SerializeField] ParticleSystem m_shootingParticle, m_impactParticle;
    [SerializeField] TrailRenderer m_bulletTrail;

    [SerializeField] LayerMask m_layerMask;

    [SerializeField] InputActionReference m_shootInputAction;

    public bool OnCooldown() { return m_cooldownTimer < 1; }

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_shootInputAction.action.performed += Shoot;
    }

    private void Update()
    {
        if (m_cooldownTimer < 1)
        {
            m_cooldownTimer += Time.deltaTime * m_fireRate;
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (!OnCooldown())
        {
            Debug.Log("Shot");
            
            m_cooldownTimer = 0;

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

                StartCoroutine(SpawnTrail(trail, hit));
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

    IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 Point, Vector3 Normal = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue))
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
        Instantiate(m_impactParticle, Point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }
}
