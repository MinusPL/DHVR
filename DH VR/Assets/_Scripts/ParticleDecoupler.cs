using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecoupler : MonoBehaviour {
    public ParticleSystem m_Particles;

    public void Decouple() {
        m_Particles.transform.parent = null;
        
        Destroy(m_Particles.gameObject, m_Particles.main.duration);
    }

}
