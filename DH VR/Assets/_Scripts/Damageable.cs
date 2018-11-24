using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Damageable : MonoBehaviour {

    public float m_MaxHealth = 1;

    private float m_CurrentHealth;

    private void Awake() {
        m_CurrentHealth = m_MaxHealth;
    }

    public void Damage(float dmg) {
        m_CurrentHealth -= dmg;

        if (m_CurrentHealth <= 0) {
            Death();
        }
    }

    void Death() {
        Destroy(gameObject);
    }
}
