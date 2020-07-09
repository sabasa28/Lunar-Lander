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
    private void Start()
    {
        highscore = GameManager.Get().highscore;
        highscoreText.text = "Highscore: " + highscore;
    }
    public void SwapToGameplayScene()
    {
        SceneManager.LoadScene(2);
    }
    
}
