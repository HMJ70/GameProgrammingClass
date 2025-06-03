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
    float speedmultiplier =  1f;

    public ParticleSystem dust;
    public ParticleSystem speedfx;

    Animator animator;

    public float JumpPower = 10f;

    public Transform checkground;
    public Vector2 checkgroundsize = new Vector2(0.5f, 0.05f);
    public LayerMask groundlayer;

    public float basegravity = 2f;
    public float maxfallspeed = 18f;
    public float fallspeedmultiplier = 2f;

    //private bool playingfootsteps = false;
    //public float footstepspeed = 0.5f;

    private void Start()
    {
        SpeedDrink.onspeedcollected += startspeedboost;
    }

    void startspeedboost(float multiplier)
    {
        StartCoroutine(speedboostcoroutine(multiplier));
    }

    private IEnumerator speedboostcoroutine(float multiplier)
    {
        speedmultiplier = multiplier;
        speedfx.Play();
        yield return new WaitForSeconds(2f);
        speedmultiplier = 1f;
        speedfx.Stop();
    }

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(horizontalmove * speed * speedmultiplier, rb2d.velocity.y);

        if (xDirection > 0 && !facingRight)
        {
            Flip();
        }
        else if (xDirection < 0 && facingRight)
        {
            Flip();
        }

        gravity();
    }
        
    public void Move(InputAction.CallbackContext context)
    {
         horizontalmove = context.ReadValue<Vector2>().x;
         dust.Play();
    }
    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        speedfx.transform.localScale = currentScale;

        facingRight = !facingRight;

        if(rb2d.velocity.y == 0)
        {
            dust.Play();
        }
    }


    private void FixedUpdate()
    {

        animator.SetFloat("xVelocity", System.Math.Abs(rb2d.velocity.x));


    }

    public void jump(InputAction.CallbackContext context)
    {
        
        if (context.performed && isgrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpPower);
            dust.Play();
            sfxmanager.Play("Jump");
        }

        if (context.canceled && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            dust.Play();
            
        }
    }

    private bool isgrounded()
    {
        if(Physics2D.OverlapBox(checkground.position, checkgroundsize,0,groundlayer))
        {
            return true;
        }
        return false;  
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(checkground.position, checkgroundsize);
    }

    private void gravity()
    {
        if (rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = basegravity * fallspeedmultiplier;
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Max(rb2d.velocity.y, -maxfallspeed));
        }
        else
        {
            rb2d.gravityScale = basegravity;
        }
    }
}
