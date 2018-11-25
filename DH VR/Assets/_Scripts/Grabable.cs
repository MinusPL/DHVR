using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabable : MonoBehaviour {
    public bool m_KeepTransform;
    public GameObject m_Highlight;
    
    private Rigidbody m_Body;
    private Collider m_Collider;

    private Vector3 m_PreviousPosition;

    private void Awake() {
        m_Body = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
    }

    private void LateUpdate() {
        m_PreviousPosition = transform.position;
    }

    public void Highlight(bool active) {
        if(m_Highlight)
            m_Highlight.SetActive(active);
    }

    public void Grab(Transform parent) {
        if (m_Body)
            m_Body.isKinematic = true;
        if (m_Collider)
            m_Collider.enabled = false;

        transform.SetParent(parent);
        if (!m_KeepTransform) {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }

    public void Release() {
        transform.parent = null;

        if (m_Body) {
            m_Body.isKinematic = false;

            var force = -((m_PreviousPosition - transform.position) / Time.deltaTime) * 0.6f;
            m_Body.AddForce(force, ForceMode.Impulse);
        }

        if (m_Collider)
            m_Collider.enabled = true;
    }
}