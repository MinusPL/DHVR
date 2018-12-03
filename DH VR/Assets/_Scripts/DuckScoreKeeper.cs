using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckScoreKeeper : MonoBehaviour {
    
    public int m_MaxScore;

    public int GetScore() {

        var controller = GetComponent<DuckController>();
        var score = m_MaxScore * controller.WayPercent();

        return Mathf.RoundToInt(score);
    }

}
