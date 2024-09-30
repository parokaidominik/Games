using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellit : EntityType
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private int xDirection = 1;
    [SerializeField] private float xMargin = 2;
    [SerializeField] private float minSpeed = 1;
    [SerializeField] private float maxSpeed = 4;
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public override void StartEntity()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        
        if (Random.Range(1, 3) == 1)
            Flip();
        else
            MoveSatellit();
    }

    private void MoveSatellit()
    {
        rb.velocity = new Vector2(xDirection * speed, rb.velocity.y);
    }

    private void Update() 
    {
        if (transform.position.x > xMargin || transform.position.x < -xMargin)
        {
            Flip();
        }
    }

    private void Flip()
    {
        xDirection *= -1;
        sr.flipX = !sr.flipX;
        MoveSatellit();
    }
}
