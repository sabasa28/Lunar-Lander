using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Get()
    {
        return instance;
    }
    float gameplayScore = 0;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetScore(float finalScore)
    {
        gameplayScore = finalScore;
    }
    public float GetScore()
    {
        return gameplayScore;
    }
}
