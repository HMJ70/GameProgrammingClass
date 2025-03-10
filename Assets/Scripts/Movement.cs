using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb2d;
    private Vector2 direction = new Vector2();

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(float xDirection)
    {
        direction = rb2d.velocity;
        direction.x = xDirection * speed;
        rb2d.velocity = direction;
    }

}
