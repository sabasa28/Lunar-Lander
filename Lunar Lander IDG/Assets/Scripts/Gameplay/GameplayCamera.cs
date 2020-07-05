using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    Ship ship;
    public float halfCameraSize;
    public float cameraZoom = -25;
    float minX = 160.0f;
    float maxX = 210.0f;
    float minXZoomed;
    float maxXZoomed;

    private void Start()
    {
        ship = FindObjectOfType<Ship>();
    }
    private void Update()
    {
        if (ship.transform.position.x >minX && ship.transform.position.x < maxX)
            transform.position = new Vector3 (ship.transform.position.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, ship.transform.position.y, cameraZoom);
    }

    public void UpdateLevelData(float minLvlX, float maxLvlX)
    {
        minX = minLvlX + halfCameraSize;
        maxX = maxLvlX - halfCameraSize;
        transform.position = new Vector3(minX, transform.position.y, cameraZoom);
    }
}
