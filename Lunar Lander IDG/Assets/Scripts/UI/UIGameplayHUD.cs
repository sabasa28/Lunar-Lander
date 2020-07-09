using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplayHUD : MonoBehaviour
{
    public List<TextMeshProUGUI> gameInfo=new List<TextMeshProUGUI>();
    public TextMeshProUGUI crashText;
    public TextMeshProUGUI landText;
    public TextMeshProUGUI checkText;
    public delegate float GetFloat();
    public GetFloat getlevelTimer;
    public Action PauseGame;
    Ship ship;
    int timeForLandResultScreen = 5;
    float displayedScore = 500;
    float displayedTime = 0;
    int displayedFuel = 0;
    float displayedAltitude = 0;
    Vector2 displayedSpeed = new Vector2(0, 0);
    void Start()
    {
        ship = FindObjectOfType<Ship>();
        ship.ShowLandResultScreen = StartLandingResultScreen;
        ship.ShowResultCheckScreen = ShowResultCheckingScreen;
    }
    void Update()
    {
        displayedScore = ship.score;
        displayedFuel = (int)ship.fuel;
        displayedAltitude = ship.altitude;
        displayedSpeed = ship.registredSpeed;
        displayedTime = (int)getlevelTimer();

        gameInfo[0].text = displayedScore + "\n"
                       + displayedTime +  "\n"
                       + displayedFuel;
        gameInfo[1].text = displayedAltitude + "\n"
                       + displayedSpeed.x + "\n"
                       + displayedSpeed.y;
    }

    public void OnClickPause()
    {
        PauseGame();
    }
    void StartLandingResultScreen(bool landed)
    {
        if (landed)
            StartCoroutine(ShowLandScreen());
        else
            StartCoroutine(ShowCrashScreen());

    }
    void ShowResultCheckingScreen()
    {
        checkText.gameObject.SetActive(true);
    }
    IEnumerator ShowCrashScreen()
    {
        if (checkText.IsActive()) checkText.gameObject.SetActive(false);
        crashText.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeForLandResultScreen);
        crashText.gameObject.SetActive(false);
        ship.OnResultsScreenExit();
    }
    IEnumerator ShowLandScreen()
    {
        checkText.gameObject.SetActive(false);
        landText.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeForLandResultScreen);
        landText.gameObject.SetActive(false);
        ship.OnResultsScreenExit();
    }
}
