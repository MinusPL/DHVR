using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RoboRyanTron.Events;

public class GameManager : MonoBehaviour {
    public List<RoundSettings> m_Rounds;
    public DuckSpawner m_Spawner;

    public GameEvent m_OnGameEnd;

    private int m_CurrentRoundIndex;
    private int m_DucksLeft;

    private int m_DucksKilledThisRound;

    private List<DuckController> m_SpawnedDucks = new List<DuckController>();

    private void Awake() {
        DuckController.OnDuckDeath += OnDuckKill;
        DuckController.OnDuckFlee += OnDuckFled;
    }

    private void Start() {
        GameStart();
    }

    IEnumerator GameRoutine() {
        //First wait
        yield return new WaitForSeconds(2f);

        while (m_CurrentRoundIndex < m_Rounds.Count) {
            Debug.Log($"Round: {m_CurrentRoundIndex + 1}");
            m_DucksKilledThisRound = 0;
            m_DucksLeft = m_Rounds[m_CurrentRoundIndex].AllDucksCount;

            while (m_DucksLeft > 0) {
                yield return new WaitForSeconds(4f);

                StageStart();
                
                yield return new WaitUntil(() => m_SpawnedDucks.Count == 0);
                Debug.Log($"End Stage. Ducks left: {m_DucksLeft}");
            }
            Debug.Log($"Killed: {m_DucksKilledThisRound} To Kill: {m_Rounds[m_CurrentRoundIndex].DucksToNextRound}");
            if (m_DucksKilledThisRound < m_Rounds[m_CurrentRoundIndex].DucksToNextRound) {
                Debug.Log("You Lost");
                break;
            }

            m_CurrentRoundIndex++;    
            Debug.Log("waiting...");
            yield return new WaitForSeconds(5f);
        }
        
        GameOver();
    }

    public void GameStart() {
        m_CurrentRoundIndex = 0;
        StartCoroutine(GameRoutine());
    }


    void StageStart() {
        //Spawn ducks
        //add them to list
        //decrement ducks left
        for (int i = 0; i < m_Rounds[m_CurrentRoundIndex].DucksPerStage && m_DucksLeft > 0; i++) {
            var duck = m_Spawner.Spawn( m_Rounds[m_CurrentRoundIndex].DucksSpeed);
            m_SpawnedDucks.Add(duck);

            m_DucksLeft--;
        }
    }

    void GameOver() {
        Debug.Log("End");
        m_OnGameEnd.Raise();
    }

    void OnDuckFled(DuckController duck) {
        m_SpawnedDucks.Remove(duck);
    }

    void OnDuckKill(DuckController duck) {
        m_SpawnedDucks.Remove(duck);
        m_DucksKilledThisRound++;
    }
}