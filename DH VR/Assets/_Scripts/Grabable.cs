using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour {
    public bool m_KeepTransform;
    public GameObject m_Highlight;

    private Rigidbody m_Body;
    private Collider m_Collider;

    private void Awake() {
        m_Body = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
    }

    public void Highlight(bool active) {
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
    }
}