using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Ship ship;
    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
    }

    void Update()
    {
        
    }
}
