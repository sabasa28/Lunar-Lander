using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISplashScreen : MonoBehaviour
{
    public GameObject splashScreen1;
    public GameObject splashScreen2;
    float timeBetweenScreens = 2.0f;
    float timeInScreens = 4.0f;

    private void Start()
    {
        StartCoroutine(ShowSplashScreens());
    }
    IEnumerator ShowSplashScreens()
    {
        yield return new WaitForSeconds(timeBetweenScreens);
        splashScreen1.SetActive(true);
        yield return new WaitForSeconds(timeInScreens);
        splashScreen1.SetActive(false);
        yield return new WaitForSeconds(timeBetweenScreens);
        splashScreen2.SetActive(true);
        yield return new WaitForSeconds(timeInScreens);
        splashScreen2.SetActive(false);
        yield return new WaitForSeconds(timeBetweenScreens);
        SceneManager.LoadScene(1);
    }
}
