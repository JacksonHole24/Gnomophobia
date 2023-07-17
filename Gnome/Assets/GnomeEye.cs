using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeEye : MonoBehaviour
{
    GameObject player = null;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    private void FixedUpdate()
    {
        transform.LookAt(player.transform.position);
    }
}
