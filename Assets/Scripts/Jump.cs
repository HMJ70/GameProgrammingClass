using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float jumppower;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        rb2d.velocity = Vector2.up * jumppower;
    }
}
