using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour {
    public Vector3 m_MovementBoxSize;
    public DuckController m_DuckPrefab;


    public DuckController Spawn(float duckSpeed) {
        var waypoints = new List<Vector3>();
        for (int i = 0; i < 5; i++) {
            float x = Random.Range(-m_MovementBoxSize.x, m_MovementBoxSize.x);
            float y = Random.Range(-m_MovementBoxSize.y, m_MovementBoxSize.y);
            float z = Random.Range(-m_MovementBoxSize.z, m_MovementBoxSize.z);

            var point = new Vector3(x, y, z) / 2f + transform.position;
            waypoints.Add(point);
        }

        float spawnX = Random.Range(-m_MovementBoxSize.x, m_MovementBoxSize.x);
        float spawnZ = Random.Range(-m_MovementBoxSize.z, m_MovementBoxSize.z);
        var spawnPoint = new Vector3(spawnX, -m_MovementBoxSize.y, spawnZ)/2f + transform.position;
        var duck = Instantiate(m_DuckPrefab, spawnPoint, Quaternion.identity);
        duck.Initialize(waypoints);
        duck.m_Speed = duckSpeed;
        
        return duck;
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, m_MovementBoxSize);
    }
}