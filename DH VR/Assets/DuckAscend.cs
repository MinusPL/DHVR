using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAscend : MonoBehaviour {
    public float m_Speed;

    // Update is called once per frame
    void Update() {
        transform.position += Vector3.up * Time.deltaTime * m_Speed;
    }
}
