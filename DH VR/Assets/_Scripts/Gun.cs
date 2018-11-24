using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public ParticleSystem m_FireParticle;
    public LineRenderer m_LineRenderer;
    public float m_LineDisplayTime;

    public void Fire() {
        StartCoroutine(HandleEffects());
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var damageable = hit.collider.GetComponent<Damageable>();
            if (damageable) {
                damageable.Damage(1);
            }
        }
    }

    IEnumerator HandleEffects() {
        m_FireParticle.Play();

        m_LineRenderer.enabled = true;
        m_LineRenderer.SetPosition(0, m_LineRenderer.transform.position);
        m_LineRenderer.SetPosition(1, m_LineRenderer.transform.position + m_LineRenderer.transform.forward * 20);
        
        yield return new WaitForSeconds(m_LineDisplayTime);

        m_LineRenderer.enabled = false;
    }
}
