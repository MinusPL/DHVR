using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour {
    public Vector3 m_MovementBoxSize;
    public Transform m_SpawnOrigin;
    public Vector2 m_SpawnPlaneSize;
    public DuckController m_DuckPrefab;
    public List<Transform> m_FleeWaypoints;


    public DuckController Spawn(float duckSpeed) {
        var waypoints = new List<Vector3>();
        for (int i = 0; i < 5; i++) {
            float x = Random.Range(-m_MovementBoxSize.x, m_MovementBoxSize.x);
            float y = Random.Range(-m_MovementBoxSize.y, m_MovementBoxSize.y);
            float z = Random.Range(-m_MovementBoxSize.z, m_MovementBoxSize.z);

            var point = new Vector3(x, y, z) / 2f + transform.position;
            waypoints.Add(point);
        }

        int index = Random.Range(0, m_FleeWaypoints.Count - 1);
        waypoints.Add(m_FleeWaypoints[index].position);
        
        float spawnX = Random.Range(-m_SpawnPlaneSize.x, m_SpawnPlaneSize.x);
        float spawnZ = Random.Range(-m_SpawnPlaneSize.y, m_SpawnPlaneSize.y);

        var spawnPoint = new Vector3(spawnX, 0, spawnZ) / 2f + m_SpawnOrigin.position;
        var duck = Instantiate(m_DuckPrefab, spawnPoint, Quaternion.identity);
        duck.Initialize(waypoints);
        duck.m_Speed = duckSpeed;

        return duck;
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, m_MovementBoxSize);
        Gizmos.color = Color.green;
        if (m_SpawnOrigin != null)
            Gizmos.DrawWireCube(m_SpawnOrigin.position, new Vector3(m_SpawnPlaneSize.x, 0, m_SpawnPlaneSize.y));
    }
}