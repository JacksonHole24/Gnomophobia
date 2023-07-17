using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeObject : MonoBehaviour
{
    [HideInInspector] public Gnome gnome;
    [SerializeField] public ParticleSystem gnomePart;

    [SerializeField] Renderer gnomeRenderer;
    [SerializeField] MeshCollider gnomeCollider;

    private float destroyTime = 1f;
    private float timer = 0;
    private bool destroy = false;

    [ContextMenu("Break")]
    public void Break()
    {
        gnomePart.Play();
        destroy = true;
        gnomeRenderer.enabled = false;
        gnomeCollider.enabled = false;
    }

    private void Update()
    {
        if (destroy)
        {
            timer += Time.deltaTime;
            if (timer > destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
