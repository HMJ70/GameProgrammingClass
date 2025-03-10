using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    private Jump JumpAction;
    private Movement movement;
    private bool isJumping;

    public bool facingRight = true;

    private void Awake()
    {
        JumpAction = GetComponent<Jump>();
        movement = GetComponent<Movement>();
    }
    void Update()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        movement.SetDirection(xDirection);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            JumpAction.Execute();
            isJumping = true;
        }

        if (xDirection > 0 && !facingRight)
        {
            Flip();
        }
        else if (xDirection < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (other.gameObject.CompareTag("GroundFromBelowAndSide"))
        {
            isJumping = true;
        }

    }

}
