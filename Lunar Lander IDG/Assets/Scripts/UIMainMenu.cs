﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public void SwapToGameplayScene()
    {
        SceneManager.LoadScene(1);
    }

}
