using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRChild : MonoBehaviour
{
    [SerializeField] Transform m_parent;

    [SerializeField] Vector3 m_localPosition, m_localRotation;//, m_localScale;

    private void LateUpdate()
    {
        transform.position = m_parent.position + m_localPosition;
        transform.rotation = Quaternion.Euler(m_parent.rotation.eulerAngles + m_localRotation);
    }
}
