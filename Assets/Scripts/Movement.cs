using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb2d;
    private Vector2 direction = new Vector2();

    Animator animator;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
    }

    public void SetDirection(float xDirection)
    {
        direction = rb2d.velocity;
        direction.x = xDirection * speed;
        rb2d.velocity = direction;
    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", System.Math.Abs(rb2d.velocity.x));
    }
}
