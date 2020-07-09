using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public int highscore;
    public TextMeshProUGUI highscoreText;
    public GameObject credits;
    private void Start()
    {
        highscore = GameManager.Get().highscore;
        highscoreText.text = "Highscore: " + highscore;
    }
    public void SwapToGameplayScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ActivateCredits()
    {
        credits.SetActive(true);
    }
    public void DeactivateCredits()
    {
        credits.SetActive(false);
    }
}
