using System;
using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Variables;
using UnityEngine;
using UnityEngine.UI;

public class RoundPlate : MonoBehaviour {
    public GameManager m_Manager;
    public Text m_RoundsText;
    public Text m_ToGoCounter;
    public Text m_KilledCounter;
    public Text m_StatusText;
    public Text m_ScoreText;

    public FloatVariable m_CurrentScore;

    public void UpdateDucksCount() {
        var toGoStr = new String('I', m_Manager.m_DucksKilledThisRound);
        m_KilledCounter.text = toGoStr;
    }

    public void UpdateScore() {
        m_ScoreText.text = $"Current Score: {m_CurrentScore.Value}";
    }

    public void UpdateRoundAndToGoText() {
        var toGoStr = new String('I', m_Manager.CurrentSettings.AllDucksCount);
        var roundStr = $"Round: {m_Manager.m_CurrentRoundIndex + 1}";

        m_RoundsText.text = roundStr;
        m_ToGoCounter.text = toGoStr;
    }

    public void ResetText() {
        m_RoundsText.text = "Round: 0";

        m_ToGoCounter.text = "";
        m_KilledCounter.text = "";
    }

    public void SetStatusText(string str) {
        m_StatusText.text = str;
    }
}
