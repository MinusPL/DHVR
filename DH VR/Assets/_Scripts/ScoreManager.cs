using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    private const string m_KeyPrefix = "Score";

    public int m_KeepedScoreCount = 10;
    public Text m_ScoreListText;
    
    private int m_CurrentScore;

    private List<ScoreData> m_ScoresData = new List<ScoreData>();
    
    
    private void Awake() {
        DuckController.OnDuckDeath += OnDuckKill;
    }

    private void Start() {
        LoadData();
    }

    void OnDuckKill(DuckController duck) {
        var scoreKeeper = duck.GetComponent<DuckScoreKeeper>();
        m_CurrentScore += scoreKeeper.GetScore();
    }

    void LoadData() {
        for (int i = 0; i < m_KeepedScoreCount; i++) {
            var key = m_KeyPrefix + i;

            if (PlayerPrefs.HasKey(key)) {
                var str = PlayerPrefs.GetString(key);
                var data = JsonUtility.FromJson<ScoreData>(str);
                
                m_ScoresData.Add(data);
            }
            else {
                m_ScoresData.Add(new ScoreData());
            }
        }
        
        UpdateText();
    }

    void UpdateText() {
        m_ScoresData = m_ScoresData.OrderByDescending(d => d.score).ToList();
        
        var builder = new StringBuilder();
        for (int i = 0; i < m_KeepedScoreCount; i++) {
            builder.Append($"{i + 1}. Score: {m_ScoresData[i].score} Round: {m_ScoresData[i].round}\n");
        }

        m_ScoreListText.text = builder.ToString();
    }

    public void SaveScore() {
        var scoreData = new ScoreData() {
            name = "test1",
            round = 0,
            score = m_CurrentScore
        };
        
        m_ScoresData.Add(scoreData);
        m_ScoresData = m_ScoresData.OrderByDescending(d => d.score).ToList();

        for (int i = 0; i < m_KeepedScoreCount; i++) {
            var key = m_KeyPrefix + i;

            var json = JsonUtility.ToJson(m_ScoresData[i]);
        
            //Debug.Log($"Saving: {json}");
        
            PlayerPrefs.SetString(key, json);
        }

        PlayerPrefs.Save();
        UpdateText();
    }
    
    [Serializable]
    struct ScoreData {
        public string name;
        public int round;
        public int score;
    }
}
