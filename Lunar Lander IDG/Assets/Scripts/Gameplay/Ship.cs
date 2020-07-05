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
    public Sprite freeFallSprite;
    public Sprite impulseSprite;
    public Sprite destroyedSprite;

    float initialForce = 200;
    public float impulseForce;
    public float rotationSpeed;
    public float gravityScale;
    public Vector2Int registredSpeed;
    public float maxLandingSpeed;
    public int altitude;
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
        rb.AddForce(transform.right * initialForce);
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
        Debug.Log(Math.Abs(registredSpeed.x) + Math.Abs(registredSpeed.y));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Math.Abs(registredSpeed.x) + Math.Abs(registredSpeed.y) > maxLandingSpeed)
        {
            SpriteState = ShipStates.destroyed;
            ChangeSprite();
        }
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
                break;
            case ShipStates.impulse:
                sr.sprite = impulseSprite;
                break;
            case ShipStates.destroyed:
                sr.sprite = destroyedSprite;
                break;
            default:
                break;
        }
    }

}
