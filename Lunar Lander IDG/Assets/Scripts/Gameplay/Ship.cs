using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public ParticleSystem particle;
    public Sprite freeFallSprite;
    public Sprite impulseSprite;
    public Sprite destroyedSprite;
    public float impulseForce;
    public float rotationSpeed;
    public float gravityScale;
    public Vector2Int registredSpeed;
    public float maxLandingSpeed;
    public int altitude;
    public int score;
    public float fuel;
    public float landingAltitude;
    public int baseScorePerLanding;
    public float currentScoreMultiplier;
    bool onLandingAltitude = false;
    const float fuelLosingSpeed = 15;
    const float fuelLostOnCrash = 200;
    const float initialForce = 200;
    const int spriteOffset = 4;
    Vector3 lastPos;
    Vector3 distanceInFrames;
    float rayDistance = 100;
    RaycastHit2D hit;
    bool ableToMove = true;
    bool displayingResults = false;
    
    public Action ChangeLevel;
    public Action<int> EndGame;
    public Action<bool> ShowLandResultScreen;
    public Action ShowResultCheckScreen;
    public Action<bool> SetCameraZoom;
    enum ShipStates
    {
        impulse,
        freeFall,
        destroyed
    }
    ShipStates ShipState = ShipStates.freeFall;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        InitialImpulse();
        particle.Stop();
    }

    private void Update()
    {
        if (rb.gravityScale != gravityScale) rb.gravityScale = gravityScale;
        
        if (Input.GetKey(KeyCode.Space) && ableToMove)
        {
            Impulse();
        }
        else if (ShipState == ShipStates.impulse)
        {
            ShipState = ShipStates.freeFall;
            ChangeSprite();
        }
        
        float rotate = -Input.GetAxisRaw("Horizontal");
        if (ableToMove) transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, rotate * rotationSpeed * Time.deltaTime));
        
        hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance);
        if (hit.collider != null)
        {
            altitude = (int)(hit.distance * 10) - spriteOffset;
            if (altitude < 0) altitude = 0;
        }
        if (onLandingAltitude && altitude > landingAltitude)
        {
            SetCameraZoom(false);
            onLandingAltitude = false;
        }
        else if (!onLandingAltitude && altitude < landingAltitude)
        {
            SetCameraZoom(true);
            onLandingAltitude = true;
        }
    }
    private void FixedUpdate()
    {
        distanceInFrames = transform.position - lastPos;
        lastPos = transform.position;
        registredSpeed.x = (int)(distanceInFrames.x * 1000);
        registredSpeed.y = (int)(distanceInFrames.y * 1000);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!displayingResults)
        {
            if (collision.gameObject.CompareTag("Stage"))
            {
                OnCrash();
            }
            if (collision.gameObject.CompareTag("LandingPlatform"))
            {
                if ((Math.Abs(registredSpeed.x) + Math.Abs(registredSpeed.y) <= maxLandingSpeed))
                {
                    StartCoroutine(CheckCorrectLanding());
                }
                else
                {
                    OnCrash();
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelLimit"))
        {
            transform.position = new Vector3(collision.gameObject.GetComponent<LevelLimit>().xToTeleportShipTo, transform.position.y, transform.position.z);
        }
    }
    public void OnLevelChange(Vector3 initialPos)
    {
        transform.position = initialPos;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        if (ShipState != ShipStates.freeFall)
        {
            ShipState = ShipStates.freeFall;
            ChangeSprite();
        }
        InitialImpulse();
    }
    void OnResultsScreenEnter()
    {
        ableToMove = false;
        displayingResults = true;
    }
    public void OnResultsScreenExit()
    {
        if (fuel <= 0)
        {
            EndGame(score);
        }
        else ChangeLevel();
        ableToMove = true;
        displayingResults = false;
    }
    void InitialImpulse()
    {
        rb.AddForce(Vector2.right * initialForce);
    }
    void Impulse()
    {
        rb.AddForce(transform.up * impulseForce);
        if (fuel > 0)
        {
            fuel -= Time.deltaTime * fuelLosingSpeed;
            if (fuel < 0) fuel = 0;
        }
        if (fuel == 0) ableToMove = false;
        if (ShipState != ShipStates.impulse)
        {
            ShipState = ShipStates.impulse;
            ChangeSprite();
        }
    }
    void ChangeSprite()
    {
        switch (ShipState)
        {
            case ShipStates.freeFall:
                sr.sprite = freeFallSprite;
                if (particle.isPlaying) particle.Stop();
                break;
            case ShipStates.impulse:
                sr.sprite = impulseSprite;
                if (!particle.isEmitting) particle.Play();
                break;
            case ShipStates.destroyed:
                sr.sprite = destroyedSprite;
                if (particle.isPlaying) particle.Stop();
                break;
        }
    }
    IEnumerator CheckCorrectLanding()
    {
        ShowResultCheckScreen();
        OnResultsScreenEnter();
        float timer = 0;
        bool correctLanding = true;
        while (timer < 3 && correctLanding)
        {
            timer += Time.deltaTime;
            if (Vector3.Angle(transform.up, Vector3.up) > 10)
            {
                correctLanding = false;
            }
            yield return null;
        }
        if (correctLanding)
            OnLanding();
        else
            OnCrash();
    }
    void OnCrash()
    {
        if (fuel > 0)
        {
            fuel -= fuelLostOnCrash;
            if (fuel < 0) fuel = 0;
        }
        ShipState = ShipStates.destroyed;
        ChangeSprite();
        ShowLandResultScreen(false);
        if(!displayingResults) OnResultsScreenEnter();
    }

    void OnLanding()
    {
        ShowLandResultScreen(true);
        score += baseScorePerLanding * (int)currentScoreMultiplier;
    }
}
