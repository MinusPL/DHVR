using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<RoundSettings> m_Rounds;
    public DuckSpawner m_Spawner;

    private int m_CurrentRoundIndex;
    private int m_DucksLeft;

    private List<DuckController> m_SpawnedDucks = new List<DuckController>();

    private void Awake() {
        DuckController.OnDuckDeath += OnDuckKill;
        DuckController.OnDuckFlee += OnDuckKill;
    }

    private void Start() {
        GameStart();
    }

    public void GameStart() {
        m_CurrentRoundIndex = 0;
        HandleRoundStart();
        Debug.Log("Game Start");
    }

    void HandleRoundStart() {
        m_DucksLeft = m_Rounds[m_CurrentRoundIndex].AllDucksCount;
        StageStart();
        Debug.Log("Round Start");

    }

    void HandleRoundEnd() {
        m_CurrentRoundIndex++;
        //if there is no more rounds
        //or player lose
        //handle game over
        if (m_CurrentRoundIndex >= m_Rounds.Count) {
            GameOver();
        }
        Debug.Log("Round End");

        HandleRoundStart();
    }

    void StageStart() {
        //Spawn ducks
        //add them to list
        //decrement ducks left
        for (int i = 0; i < m_Rounds[m_CurrentRoundIndex].DucksPerStage; i++) {
            var duck = m_Spawner.Spawn( m_Rounds[m_CurrentRoundIndex].DucksSpeed);
            m_SpawnedDucks.Add(duck);

            m_DucksLeft--;
        }
        Debug.Log("Stage Start");

        

    }

    void StageEnd() {
        //check if there is more ducks to spawn
        //if so prepare to spawn
        //else
        //Handle round end
        Debug.Log("Stage End");

        if (m_DucksLeft > 0) {
            StageStart();
        }
        else {
            HandleRoundEnd();
        }
        
    }

    void GameOver() {
        Debug.Log("End");
    }

    void OnDuckKill(DuckController duck) {
        m_SpawnedDucks.Remove(duck);
        if (m_SpawnedDucks.Count == 0) {
            StageEnd();
        }
    }
}