using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplayHUD : MonoBehaviour
{
    public List<TextMeshProUGUI> gameInfo=new List<TextMeshProUGUI>();
    Ship ship;
    float displayedScore =500;
    float displayedTime  =0;
    float displayedFuel  =0;
    float displayedAltitude =0;
    Vector2 displayedSpeed = new Vector2(0, 0);
    void Start()
    {
        ship = FindObjectOfType<Ship>();
    }
    void Update()
    {
        displayedAltitude = ship.altitude;
        displayedSpeed = ship.registredSpeed;
        gameInfo[0].text = displayedScore + "\n"
                       + displayedTime +  "\n"
                       + displayedFuel;
        gameInfo[1].text = displayedAltitude + "\n"
                       + displayedSpeed.x + "\n"
                       + displayedSpeed.y;
    }
}
