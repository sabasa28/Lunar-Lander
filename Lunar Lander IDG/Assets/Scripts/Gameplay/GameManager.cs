using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public int highscore;
    public static GameManager Get()
    {
        return instance;
    }
    int gameplayScore = 0;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
     
        ReadHighscore();
    }
    public void SetScore(int finalScore)
    {
        gameplayScore = finalScore;
        CheckForNewHighscore(gameplayScore);
    }
    public float GetScore()
    {
        return gameplayScore;
    }
    void CheckForNewHighscore(int newScore)
    {
        if (newScore > highscore)
        {
            highscore = newScore;
            SaveHighscore();
        }
    }
    void SaveHighscore()
    {
        FileStream fs = File.OpenWrite("highscore.dat");
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(highscore);
        bw.Close();
        fs.Close();
    }
    void ReadHighscore()
    {
        FileStream fs = File.OpenRead("highscore.dat");
        BinaryReader br = new BinaryReader(fs);
        highscore = br.ReadInt32();
        br.Close();
        fs.Close();
    }
}
