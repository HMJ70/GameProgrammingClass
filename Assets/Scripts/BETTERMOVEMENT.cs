using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BETTERMOVEMENT : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed = 5f;

    public bool facingRight = true;

    float horizontalmove;
    Animator animator;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(horizontalmove * speed, rb2d.velocity.y);

        if (xDirection > 0 && !facingRight)
        {
            Flip();
        }
        else if (xDirection < 0 && facingRight)
        {
            Flip();
        }
    }
        
    public void Move(InputAction.CallbackContext context)
    {
        horizontalmove = context.ReadValue<Vector2>().x;
    }
    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        facingRight = !facingRight;
    }
    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", System.Math.Abs(rb2d.velocity.x));
    }
}
