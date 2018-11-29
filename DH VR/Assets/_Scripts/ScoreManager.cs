using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private const string m_KeyPrefix = "Score";

    public int m_KeepedScoreCount = 10;
    
    private int m_CurrentScore;
    
    
    private void Awake() {
        DuckController.OnDuckDeath += OnDuckKill;
    }

    void OnDuckKill(DuckController duck) {
        var scoreKeeper = duck.GetComponent<DuckScoreKeeper>();
        m_CurrentScore += scoreKeeper.GetScore();
    }

    void LoadData() {
        for (int i = 0; i < m_KeepedScoreCount; i++) {
            var key = m_KeyPrefix + i;

            if (PlayerPrefs.HasKey(key)) {
                
            }
        }
    }

    public void SaveScore() {
        var scoreData = new ScoreData() {
            name = "test1",
            round = 0,
            score = m_CurrentScore
        };

        var json = JsonUtility.ToJson(scoreData);
        
        Debug.Log($"Saving: {json}");
        
        PlayerPrefs.SetString("test1", json);
        PlayerPrefs.Save();
    }
    
    [Serializable]
    struct ScoreData {
        public string name;
        public int round;
        public int score;
    }
}
