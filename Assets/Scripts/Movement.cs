using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField, Range(5f, 20f)]
    private float speed;

    private Rigidbody2D rb2d;
    private float direction;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetDirection(Input.GetAxisRaw("Horizontal"));
       
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 velocity = rb2d.velocity;
        velocity.x = direction * speed;
        rb2d.velocity = velocity;
    }

    public void SetDirection(float direction)
    {
        this.direction = direction;
    }

   
}
