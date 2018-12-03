using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter : MonoBehaviour {
    public Text m_Text;

    public void SetText(string text) {
        m_Text.text = text;
    }
}
