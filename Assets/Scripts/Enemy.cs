using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform Player;
    public float chasespeed = 2f;
    public float jumpforce = 2f;
    public LayerMask groundlayer;

    private Rigidbody2D rb2d;
    private bool isgrounded;
    private bool shouldjump;

    public bool facingRight = true;

    public int damage = 1;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player").GetComponent <Transform>();
    }

    void Update()
    {
        isgrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundlayer);

        float direction = Mathf.Sign(Player.position.x - transform.position.x);

        bool isplayerabove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << Player.gameObject.layer);

        if(isgrounded)
        {
            rb2d.velocity = new Vector2(direction * chasespeed, rb2d.velocity.y);

            RaycastHit2D groundinfront = Physics2D.Raycast(transform.position, new Vector2(direction,0), 2f, groundlayer);

            RaycastHit2D gapahead = Physics2D.Raycast(transform.position + new Vector3(direction,0,0), Vector2.down, 2f, groundlayer);

            RaycastHit2D platformabove = Physics2D.Raycast(transform.position, Vector2.up, 5f, groundlayer);

            if (!groundinfront.collider && !gapahead.collider)
            {
                shouldjump = true;
            }
            else if (isplayerabove && platformabove.collider)
            {
                shouldjump = true;
            }
        }

        float xDirection = Input.GetAxisRaw("Horizontal");
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

    private void FixedUpdate()
    {
        if (isgrounded && shouldjump)
        {
            shouldjump = false;
            Vector2 direction = (Player.position - transform.position).normalized;

            Vector2 jumpdirection = direction * jumpforce;

            rb2d.AddForce(new Vector2(jumpdirection.x, jumpforce), ForceMode2D.Impulse);
        }
    }


}
