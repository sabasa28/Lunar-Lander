using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndscreen : MonoBehaviour
{
    public TextMeshProUGUI score;
    private void Start()
    {
        score.text = "Final score: " + GameManager.Get().GetScore();
    }
    public void SwapToMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }
    public void SwapToGameplayScene()
    {
        SceneManager.LoadScene(2);
    }
}
