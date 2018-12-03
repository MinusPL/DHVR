using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
    public int m_MaxAmmo = 3;
    public bool m_InfiniteAmmo { get; set; }

    public List<Image> m_AmmoImages;

    public ParticleSystem m_FireParticle;
    public LineRenderer m_LineRenderer;
    public float m_LineDisplayTime;

    public float m_FireRadius;

    private int m_CurrentAmmo;
    private int m_ImageToDisableIndex;

    private RaycastHit[] m_FireResults;

    private void Awake() {
        m_FireResults = new RaycastHit[5];
    }

    private void Start() {
        RefreshAmmo();
        m_InfiniteAmmo = true;
    }

    public void Fire() {
        if (m_CurrentAmmo <= 0)
            return;

        StartCoroutine(HandleEffects());

        if (!m_InfiniteAmmo) {
            m_CurrentAmmo--;

            m_AmmoImages[m_ImageToDisableIndex].gameObject.SetActive(false);
            m_ImageToDisableIndex--;
        }

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.SphereCastNonAlloc(ray, m_FireRadius, m_FireResults) > 0) {
            foreach (var hit in m_FireResults) {
                var damageable = hit.collider?.GetComponentInParent<Damageable>();
                if (damageable) {
                    damageable.Damage(1);
                }
            }
        }
    }

    void RefreshImages() {
        while (m_AmmoImages.Count < m_CurrentAmmo) {
            var obj = m_AmmoImages[0];

            m_AmmoImages.Add(Instantiate(obj, obj.transform.parent));
        }

        for (int i = 0; i < m_CurrentAmmo; i++) {
            m_AmmoImages[i].gameObject.SetActive(true);
        }

        m_ImageToDisableIndex = m_AmmoImages.Count - 1;
    }

    IEnumerator HandleEffects() {
        m_FireParticle.Play();

        m_LineRenderer.enabled = true;
        m_LineRenderer.SetPosition(0, m_LineRenderer.transform.position);
        m_LineRenderer.SetPosition(1, m_LineRenderer.transform.position + m_LineRenderer.transform.forward * 20);

        yield return new WaitForSeconds(m_LineDisplayTime);

        m_LineRenderer.enabled = false;
    }

    public void RefreshAmmo() {
        m_CurrentAmmo = m_MaxAmmo;
        RefreshImages();
    }
}