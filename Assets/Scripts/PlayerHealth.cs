using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxhealth = 5;
    private int currhealth;

    public HealthUI healthUI;
    public BETTERMOVEMENT movement;

    private SpriteRenderer spriterenderer;

    private Rigidbody2D rb2d;
    public float knockbackForce = 15f;
    public Vector2 knockbackDirection = new Vector2(-1f, 1f);

    public static event Action ondeath;

    public static PlayerHealth Instance;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        currhealth = maxhealth;
        healthUI.setmaxhearts(maxhealth);

        spriterenderer = GetComponent<SpriteRenderer>();

        movement = GetComponent<BETTERMOVEMENT>();
        rb2d = GetComponent<Rigidbody2D>();


        GameController.onreset += resetHP;
        Heal.onhealthcollect += healing;


    }

 

    void resetHP()
    {
        currhealth = maxhealth;
        healthUI.setmaxhearts(maxhealth);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.damage);
            sfxmanager.Play("PlayerHit");
        }
        Trap trap = collision.GetComponent<Trap>();
        if(trap && trap.damage > 0)
        {
            TakeDamage(trap.damage);
            sfxmanager.Play("PlayerHit");
        }
        else if(trap)
        {
            sfxmanager.Play("Bounce");
        }

    }

    void healing(int amount)
    {
        currhealth += amount;
        if(currhealth > maxhealth)
        {
            currhealth = maxhealth;
        }
        healthUI.updateheart(currhealth);
    }


    private void TakeDamage(int damage)
    {

        currhealth -= damage;
        healthUI.updateheart(currhealth);

        StartCoroutine(FlashRed());

        ApplyKnockback();

        if (currhealth <= 0)
        {
            ondeath.Invoke();
        }
    }
    private void ApplyKnockback()
    {
        float direction = movement.facingRight ? -1f : 1f;

        Vector2 force = new Vector2(knockbackForce * direction, knockbackForce * 0.5f);
        rb2d.velocity = Vector2.zero; 
        rb2d.AddForce(force, ForceMode2D.Impulse);
    }


    private IEnumerator FlashRed()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriterenderer.color = Color.white;
    }
}
