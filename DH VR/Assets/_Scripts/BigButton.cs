using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BigButton : MonoBehaviour {
    public UnityEvent m_OnPush;

    public Transform m_ButtonAnimTransform; 
    
    private bool m_Pressing;
    
    public void Push() {
        if(m_Pressing)
            return;
        
        m_OnPush.Invoke();
        StartCoroutine(SimpleAnimation());
    }

    IEnumerator SimpleAnimation() {
        m_Pressing = true;
        m_ButtonAnimTransform.localPosition -= transform.up * 0.1f;
        
        yield return new WaitForSeconds(0.5f);
        
        m_ButtonAnimTransform.localPosition += transform.up * 0.1f;
        m_Pressing = false;
    }

}
