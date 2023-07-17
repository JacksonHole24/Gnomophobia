using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRChild : MonoBehaviour
{
    [SerializeField] Transform m_parent;
    public Transform GetParent() { return m_parent; }

    private void LateUpdate()
    {
        transform.position = m_parent.position;
        transform.rotation = m_parent.rotation;
    }
}
