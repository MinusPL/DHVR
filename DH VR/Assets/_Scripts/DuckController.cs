using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour {
    public float m_NextWaipointTreshold;
    public float m_Speed;

    public static event System.Action<DuckController> OnDuckDeath;

    private List<Vector3> m_Waipoints;
    private int m_CurrentWaitpointIndex;

    public void Initialize(List<Vector3> waipoints) {
        m_Waipoints = waipoints;
    }

    private void Update() {
        var target = m_Waipoints[m_CurrentWaitpointIndex];
        var dir = (target - transform.position).normalized;

        transform.position += dir * m_Speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(target);
        
        if (Vector3.SqrMagnitude(transform.position - target) < m_NextWaipointTreshold * m_NextWaipointTreshold) {
            m_CurrentWaitpointIndex++;
            if (m_CurrentWaitpointIndex >= m_Waipoints.Count) {
                Flee();
            }
        }
    }

    private void Flee() {
        Destroy(gameObject);
    }

    public void Death() {
        OnDuckDeath?.Invoke(this);
    }

    private void OnDrawGizmos() {
        if (m_Waipoints != null)
            for (int i = 0; i < m_Waipoints.Count - 1; i++) {
                Gizmos.DrawLine(m_Waipoints[i], m_Waipoints[i + 1]);
            }
    }
}