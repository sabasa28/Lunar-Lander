using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public Sprite freeFallSprite;
    public Sprite impulseSprite;
    public Sprite destroyedSprite;

    public float impulseForce;
    public float rotationSpeed;
    enum shipStates
    {
        impulse,
        freeFall,
        destroyed
    }
    shipStates SpriteState = shipStates.freeFall;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Impulse();
        }
        else if (SpriteState == shipStates.impulse)
        {
            SpriteState = shipStates.freeFall;
            ChangeSprite();
        }
        float rotate = -Input.GetAxisRaw("Horizontal");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, rotate * Time.deltaTime * rotationSpeed));
    }
    void Impulse()
    {
        rb.AddForce(transform.up * impulseForce);
        if (SpriteState != shipStates.impulse)
        {
            SpriteState = shipStates.impulse;
            ChangeSprite();
        }
    }
    void ChangeSprite()
    {
        switch (SpriteState)
        {
            case shipStates.freeFall:
                sr.sprite = freeFallSprite;
                break;
            case shipStates.impulse:
                sr.sprite = impulseSprite;
                break;
            case shipStates.destroyed:
                sr.sprite = destroyedSprite;
                break;
            default:
                break;
        }
    }

}
