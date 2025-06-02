using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float bounceforce = 10f;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Monster") || collision.CompareTag("Bullet"))
        {
            handleplayerbounce(collision.gameObject);
        }
    }

    private void handleplayerbounce(GameObject player)
    {
        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();

        if(rb2d)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);

            rb2d.AddForce(Vector2.up * bounceforce, ForceMode2D.Impulse);
        }
    }

}
