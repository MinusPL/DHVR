using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour {
    public float m_NextWaypointTreshold;
    public float m_Speed;

    public static event System.Action<DuckController> OnDuckDeath;
    public static event System.Action<DuckController> OnDuckFlee;

    private List<Vector3> m_Waypoints;
    private int m_CurrentWaypointIndex;

    public void Initialize(List<Vector3> waypoints) {
        m_Waypoints = waypoints;
    }

    private void Update() {
        var target = m_Waypoints[m_CurrentWaypointIndex];
        var dir = (target - transform.position).normalized;

        transform.position += dir * m_Speed * Time.deltaTime;
        transform.LookAt(target);
        
        if (Vector3.SqrMagnitude(transform.position - target) < m_NextWaypointTreshold * m_NextWaypointTreshold) {
            m_CurrentWaypointIndex++;
            if (m_CurrentWaypointIndex >= m_Waypoints.Count) {
                Flee();
            }
        }
    }

    public float WayPercent() {
        return 1f - ((float) m_CurrentWaypointIndex / m_Waypoints.Count);
    }

    private void Flee() {
        OnDuckFlee?.Invoke(this);
        Destroy(gameObject);
    }

    public void Death() {
        OnDuckDeath?.Invoke(this);
    }

    public void JustDestroy() {
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        if (m_Waypoints != null)
            for (int i = 0; i < m_Waypoints.Count - 1; i++) {
                Gizmos.DrawLine(m_Waypoints[i], m_Waypoints[i + 1]);
            }
    }
}