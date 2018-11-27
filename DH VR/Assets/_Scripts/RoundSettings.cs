using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RoundSettings : ScriptableObject {
    public int AllDucksCount;
    public int DucksPerStage;
    public int DucksToNextRound;
    public float DucksSpeed;
}
