using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public Ship ship;
    public GameplayCamera gameplayCamera;
    public UIGameplayHUD gameplayHUD;
    public float timeInLvl = 0;
    float playerXOffsetToSpawn = 3.5f;
    struct lvl
    {
        public Vector2 initialPlayerPos;
        public float limitXmin;
        public float limitXmax;
    }
    lvl lvl1;
    lvl lvl2;
    lvl lvl3;
    List<lvl> level = new List<lvl>(3);
    int currentLvl = 0;
    void Start()
    {
        ship = FindObjectOfType<Ship>();
        gameplayCamera = FindObjectOfType<GameplayCamera>();
        gameplayHUD = FindObjectOfType<UIGameplayHUD>();

        InitializeLevels();
        currentLvl = 0;
        StartCoroutine(LevelTimer());

        ship.transform.position = lvl1.initialPlayerPos;
        ship.ChangeLevel = ChangeLevel;
        ship.EndGame = LoadEndscreen;
        gameplayCamera.UpdateLevelData(lvl1.limitXmin, lvl1.limitXmax);
        gameplayHUD.getlevelTimer = PassLevelTimer;
        gameplayHUD.PauseGame = PauseGame;
    }
    void InitializeLevels()
    {
        lvl1.limitXmin = 135.5f;
        lvl1.limitXmax = 195.0f;
        lvl1.initialPlayerPos = new Vector3(lvl1.limitXmin + playerXOffsetToSpawn, 125);
        lvl2.limitXmin = 200.0f;
        lvl2.limitXmax = 260.0f;
        lvl2.initialPlayerPos = new Vector3(lvl2.limitXmin + playerXOffsetToSpawn, 125);
        lvl3.limitXmin = 265.0f;
        lvl3.limitXmax = 325.0f;
        lvl3.initialPlayerPos = new Vector3(lvl3.limitXmin + playerXOffsetToSpawn, 125);
        level.Add(lvl1);
        level.Add(lvl2);
        level.Add(lvl3);
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }
    IEnumerator LevelTimer()
    {
        while (true)
        {
            timeInLvl += Time.deltaTime;
            yield return null;
        }
    }
    void ChangeLevel()
    {
        if (currentLvl == level.Count - 1) currentLvl = 0;
        else currentLvl ++;
        timeInLvl = 0;
        ship.OnLevelChange(level[currentLvl].initialPlayerPos);
        gameplayCamera.UpdateLevelData(level[currentLvl].limitXmin, level[currentLvl].limitXmax);
    }

    float PassLevelTimer()
    {
        return timeInLvl;
    }

    void LoadEndscreen(float score)
    {
        GameManager.Get().SetScore(score);  //Tirar un evento al GameManager y que el tome el score de aca sino
        SceneManager.LoadScene(3);
    }
}
