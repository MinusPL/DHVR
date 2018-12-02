using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabable : MonoBehaviour {
    public bool m_KeepTransform;
    public GameObject m_Highlight;
    public List<Collider> m_CollidersToDisable;
    public bool m_AddThisCollider = true;
    
    private Rigidbody m_Body;

    private Vector3 m_PreviousPosition;

    private void Awake() {
        m_Body = GetComponent<Rigidbody>();
        if (m_AddThisCollider) {
            var coll = GetComponent<Collider>();
            if (coll) {
                m_CollidersToDisable.Add(coll);
            }
        }
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
        foreach (var coll in m_CollidersToDisable) {
            coll.enabled = false;
        }

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

        foreach (var coll in m_CollidersToDisable) {
            coll.enabled = true;
        }
    }
}