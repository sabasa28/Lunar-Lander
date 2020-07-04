using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndscreen : MonoBehaviour
{
    public void SwapToMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void SwapToGameplayScene()
    {
        SceneManager.LoadScene(1);
    }
}
