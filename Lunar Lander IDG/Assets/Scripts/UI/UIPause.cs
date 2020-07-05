using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void ResumeGameplay()
    {
        SceneManager.UnloadSceneAsync(4);
        Time.timeScale = 1;
    }
}
