using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LandingPlatforms : MonoBehaviour
{
    public float scoreMultiplier;
    Ship ship;
    private void Start()
    {
        ship = FindObjectOfType<Ship>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ship.currentScoreMultiplier = scoreMultiplier;
        }
    }
}
