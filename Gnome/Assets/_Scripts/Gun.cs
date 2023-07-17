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

    [SerializeField] GameObject m_hitMarkerPrefab;
    [SerializeField] bool m_showLazerSight;
    Transform m_lazerPoint;

    Animator m_animator;

    [SerializeField] TextMeshPro m_ammoDisplayTMP;
    [SerializeField] Transform m_bulletSpawnPoint;
    [SerializeField] ParticleSystem m_shootingParticle, m_impactParticle;
    [SerializeField] TrailRenderer m_bulletTrail;

    [SerializeField] LayerMask m_layerMask;

    [SerializeField] InputActionReference m_shootInputAction;
    [SerializeField] InputActionReference m_reloadInputAction;

    Player m_player;
    bool m_shootNextFrame;

    public bool OnCooldown() { return m_cooldownTimer < 1; }

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_ammoAmount = m_maxAmmoAmount;

        m_player = FindObjectOfType<Player>();

        m_shootInputAction.action.performed += Shoot;
        m_reloadInputAction.action.performed += Reload;

        m_lazerPoint = Instantiate(m_hitMarkerPrefab).transform;
    }

    private void Update()
    {
        if (m_cooldownTimer < 1)
        {
            m_cooldownTimer += Time.deltaTime * m_fireRate;
        }
    }

    private void LateUpdate()
    {
        if (m_showLazerSight) DisplayLazerSight();

        if (m_shootNextFrame)
        {
            m_shootNextFrame = false;
            Shoot();
        }
    }

    #region Display
    void DisplayLazerSight()
    {
        if (Physics.Raycast(m_bulletSpawnPoint.position, m_bulletSpawnPoint.forward, out RaycastHit hit, float.MaxValue, m_layerMask))
        {
            m_lazerPoint.position = hit.point;
        }
    }

    void DisplayAmmo()
    {
        m_ammoDisplayTMP.text = m_ammoAmount.ToString() + "/" + m_maxAmmoAmount.ToString();
    }
    #endregion

    #region Shooting/Reloading
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
        m_shootNextFrame = true;
    }

    public void Shoot()
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

                TrailRenderer trail = Instantiate(m_bulletTrail, m_bulletSpawnPoint.position, Quaternion.identity);

                RaycastHit hit;
                if (Physics.Raycast(m_bulletSpawnPoint.position, m_bulletSpawnPoint.forward, out hit, float.MaxValue, m_layerMask))
                {
                    Debug.Log("Object hit: " + hit.transform);

                    StartCoroutine(SpawnTrail(trail, hit));

                    GnomeObject gnome = hit.transform.GetComponent<GnomeObject>();
                    if (gnome)
                    {
                        m_player.AddScore(gnome.gnome.gnomeValue);
                        gnome.Break();
                    }
                }
                else
                {
                    StartCoroutine(SpawnTrail(trail, m_bulletSpawnPoint.position + (m_bulletSpawnPoint.forward * 100)));
                }
            }

        }

    }
    #endregion

    #region Trails
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
    #endregion
}
