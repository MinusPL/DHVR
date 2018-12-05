using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RoboRyanTron.Events;

public class GameManager : MonoBehaviour {
    public List<RoundSettings> m_Rounds;
    public DuckSpawner m_Spawner;

    [Header("Events")] 
    public GameEvent m_OnGameStart;
    public GameEvent m_OnGameStop;
    public GameEvent m_OnGameWon;
    public GameEvent m_OnGameLoose;
    public GameEvent m_OnStageStart;
    public GameEvent m_OnRoundStart;
    public GameEvent m_OnDuckKilled;

    public int m_CurrentRoundIndex { get; private set; }
    private int m_DucksLeft;

    public int m_DucksKilledThisRound { get; private set; }

    public RoundSettings CurrentSettings => m_Rounds[m_CurrentRoundIndex];

    private List<DuckController> m_SpawnedDucks = new List<DuckController>();

    private bool m_GameStarted = false;

    private void Awake() {
        DuckController.OnDuckDeath += OnDuckKill;
        DuckController.OnDuckFlee += OnDuckFled;
    }

    private void Start() {
        //GameStart();
    }

    IEnumerator GameRoutine() {
        //First wait
        yield return new WaitForSeconds(1f);

        bool lost = false;
        m_OnGameStart.Raise();
        
        while (m_CurrentRoundIndex < m_Rounds.Count) {
            Debug.Log($"Round: {m_CurrentRoundIndex + 1}");
            m_OnRoundStart.Raise();
            m_DucksKilledThisRound = 0;
            m_DucksLeft = CurrentSettings.AllDucksCount;

            while (m_DucksLeft > 0) {
                yield return new WaitForSeconds(2f);

                StageStart();
                
                yield return new WaitUntil(() => m_SpawnedDucks.Count == 0);
                Debug.Log($"End Stage. Ducks left: {m_DucksLeft}");
            }
            Debug.Log($"Killed: {m_DucksKilledThisRound} To Kill: {CurrentSettings.DucksToNextRound}");
            if (m_DucksKilledThisRound < CurrentSettings.DucksToNextRound) {
                Debug.Log("You Lost");
                m_OnGameLoose.Raise();
                lost = true;
                break;
            }

            m_CurrentRoundIndex++;    
            Debug.Log("waiting...");
            yield return new WaitForSeconds(5f);
        }
        
        if(!lost)
            GameWon();

        m_GameStarted = false;
    }

    public void StartStopGame() {
        if (m_GameStarted) {
            m_GameStarted = false;
            StopAllCoroutines();

            m_DucksKilledThisRound = 0;
            m_OnGameStop.Raise();
        }
        else {
            GameStart();
        }
    }

    void GameStart() {
        m_GameStarted = true;
        
        m_CurrentRoundIndex = 0;
        StartCoroutine(GameRoutine());
    }


    void StageStart() {
        //Spawn ducks
        //add them to list
        //decrement ducks left
        m_OnStageStart.Raise();
        for (int i = 0; i < CurrentSettings.DucksPerStage && m_DucksLeft > 0; i++) {
            var duck = m_Spawner.Spawn( CurrentSettings.DucksSpeed);
            m_SpawnedDucks.Add(duck);

            m_DucksLeft--;
        }
    }

    void GameWon() {
        Debug.Log("End");
        m_OnGameWon.Raise();
    }

    void OnDuckFled(DuckController duck) {
        m_SpawnedDucks.Remove(duck);
    }

    void OnDuckKill(DuckController duck) {
        m_SpawnedDucks.Remove(duck);
        m_DucksKilledThisRound++;
        
        m_OnDuckKilled.Raise();
    }
}