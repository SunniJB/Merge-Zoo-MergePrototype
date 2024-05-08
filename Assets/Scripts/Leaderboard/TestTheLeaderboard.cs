using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddLeaderboardScore : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
      Invoke("AddScoreTest",2f);
    }
    
    void AddScoreTest()
    {
        Leaderboard.CheckAndSubmitNewScore(6);

    }

    
}
