using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Ship ship;
    float minX = 160.0f;
    float maxX = 210.0f;
    float minXZoomed;
    float maxXZoomed;

    private void Start()
    {
        ship = FindObjectOfType<Ship>();
        transform.position= new Vector3(minX, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        if (ship.transform.position.x >minX && ship.transform.position.x < maxX)
            transform.position = new Vector3 (ship.transform.position.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, ship.transform.position.y, transform.position.z);
    }
}
