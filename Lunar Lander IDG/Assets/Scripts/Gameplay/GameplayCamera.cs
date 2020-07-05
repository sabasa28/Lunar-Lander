using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    Ship ship;
    public float halfCameraSize;
    float halfCameraSizeNotZoomed = 26.5f;
    float halfCameraSizeZoomed = 10.5f;
    const float notZoomed = -25;
    const float zoomed = -10;
    public float cameraZoom = notZoomed;
    float levelMinX;
    float levelMaxX;
    float minX = 160.0f;
    float maxX = 210.0f;
    float minXZoomed;
    float maxXZoomed;

    private void Start()
    {
        ship = FindObjectOfType<Ship>();
        ship.SetCameraZoom = SetZoom;
    }
    private void Update()
    {
        if (ship.transform.position.x < minX)
            transform.position = new Vector3(minX, ship.transform.position.y, cameraZoom);
        else if (ship.transform.position.x > maxX)
            transform.position = new Vector3(maxX, ship.transform.position.y, cameraZoom);
        else 
            transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, cameraZoom);
    }

    public void UpdateLevelData(float minLvlX, float maxLvlX)
    {
        levelMinX = minLvlX;
        levelMaxX = maxLvlX;
        UpdateLimits();
        transform.position = new Vector3(minX, transform.position.y, cameraZoom);
    }

    public void SetZoom(bool zoom)
    {
        if (zoom)
        {
            halfCameraSize = halfCameraSizeZoomed;
            cameraZoom = zoomed;
            UpdateLimits();
        }
        else
        {
            halfCameraSize = halfCameraSizeNotZoomed;
            cameraZoom = notZoomed;
            UpdateLimits();
        }
    }

    void UpdateLimits()
    {
        minX = levelMinX + halfCameraSize;
        maxX = levelMaxX - halfCameraSize;
    }
}
