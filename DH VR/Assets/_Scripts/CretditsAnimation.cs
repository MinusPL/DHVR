using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CretditsAnimation : MonoBehaviour {
    private Animator m_Animator;

    private void Awake() {
        m_Animator = GetComponent<Animator>();
    }

    public void Show(bool show) {
        m_Animator.SetBool("Show", show);
    }

}
