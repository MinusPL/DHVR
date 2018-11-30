using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundPlate : MonoBehaviour {
    public GameManager m_Manager;
    public Text m_RoundsText;
    public Text m_ToGoCounter;
    public Text m_KilledCounter;
    public Text m_StatusText;

    public void UpdateDucksCount() {
        var toGoStr = new String('I', m_Manager.m_DucksKilledThisRound);
        m_KilledCounter.text = toGoStr;
    }

    public void UpdateRoundAndToGoText() {
        var toGoStr = new String('I', m_Manager.CurrentSettings.AllDucksCount);
        var roundStr = $"Round: {m_Manager.m_CurrentRoundIndex + 1}";

        m_RoundsText.text = roundStr;
        m_ToGoCounter.text = toGoStr;
    }

    public void SetStatusText(string str) {
        m_StatusText.text = str;
    }
}
