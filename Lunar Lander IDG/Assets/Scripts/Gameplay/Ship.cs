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
    public float score;
    public float fuel;

    const float initialForce = 200;
    const int spriteOffset = 4;
    Vector3 lastPos;
    Vector3 distanceInFrames;
    float rayDistance = 100;
    RaycastHit2D hit;
    
    enum ShipStates
    {
        impulse,
        freeFall,
        destroyed
    }
    ShipStates SpriteState = ShipStates.freeFall;
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
        if (Input.GetKey(KeyCode.Space))
        {
            Impulse();
        }
        else if (SpriteState == ShipStates.impulse)
        {
            SpriteState = ShipStates.freeFall;
            ChangeSprite();
        }
        float rotate = -Input.GetAxisRaw("Horizontal");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, rotate * Time.deltaTime * rotationSpeed));

        hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance);
        if (hit.collider!=null) altitude = (int)(hit.distance * 10) - spriteOffset;
        if (altitude < 0) altitude = 0;
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
        if (Math.Abs(registredSpeed.x) + Math.Abs(registredSpeed.y) > maxLandingSpeed)
        {
            SpriteState = ShipStates.destroyed;
            ChangeSprite();
        }
    }
    void InitialImpulse()
    {
        rb.AddForce(Vector2.right * initialForce);
    }
    void Impulse()
    {
        rb.AddForce(transform.up * impulseForce);
        if (SpriteState != ShipStates.impulse)
        {
            SpriteState = ShipStates.impulse;
            ChangeSprite();
        }
    }
    void ChangeSprite()
    {
        switch (SpriteState)
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
            default:
                break;
        }
    }

    public void OnLevelChange(Vector3 initialPos)
    {
        transform.position = initialPos;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        if (SpriteState != ShipStates.freeFall)
        {
            SpriteState = ShipStates.freeFall;
            ChangeSprite();
        }
        InitialImpulse();
    }

}
