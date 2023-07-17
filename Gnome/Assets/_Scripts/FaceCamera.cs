using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Transform m_cam;
    private void Awake() { m_cam = Camera.main.transform; }
    private void LateUpdate() { transform.forward = m_cam.forward; }
}
