using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Ship ship;
    public GameplayCamera gameplayCamera;
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
    lvl currentLvl;
    void Start()
    {
        lvl1.limitXmin = 135.5f;
        lvl1.limitXmax = 210.0f;
        lvl1.initialPlayerPos = new Vector3(lvl1.limitXmin + playerXOffsetToSpawn,125);
        lvl2.limitXmin = 160.0f;
        lvl2.limitXmax = 210.0f;
        lvl2.initialPlayerPos = new Vector3(lvl2.limitXmin + playerXOffsetToSpawn, 125);
        //lvl3.initialPlayerPos = new Vector3(210, 125);
        //lvl3.limitXmin = 160.0f;
        //lvl3.limitXmax = 210.0f;


        currentLvl = lvl1;
        ship = FindObjectOfType<Ship>();
        ship.transform.position = lvl1.initialPlayerPos;
        gameplayCamera = FindObjectOfType<GameplayCamera>();
        gameplayCamera.UpdateLevelData(lvl1.limitXmin, lvl1.limitXmax);
        
        StartCoroutine(LevelTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeLevel();
        }
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
        currentLvl = lvl2;
        timeInLvl = 0;
        ship.OnLevelChange(currentLvl.initialPlayerPos);
        gameplayCamera.UpdateLevelData(currentLvl.limitXmin, currentLvl.limitXmax);
    }
}
