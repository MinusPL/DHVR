using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BigButton : MonoBehaviour {
    public UnityEvent m_OnPush;

    public Transform m_ButtonAnimTransform;
    
    public void Push() {
        m_OnPush.Invoke();
        StartCoroutine(SimpleAnimation());
    }

    IEnumerator SimpleAnimation() {
        m_ButtonAnimTransform.localPosition -= transform.up * 0.1f;
        
        yield return new WaitForSeconds(0.5f);
        
        m_ButtonAnimTransform.localPosition += transform.up * 0.1f;
    }

}
