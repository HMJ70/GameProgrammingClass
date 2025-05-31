using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxhealth = 5;
    private int currhealth;

    public HealthUI healthUI;

    private SpriteRenderer spriterenderer;

    void Start()
    {
        currhealth = maxhealth;
        healthUI.setmaxhearts(maxhealth);

        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.damage);
        }

    }

    private void TakeDamage(int damage)
    {
        currhealth -= damage;
        healthUI.updateheart(currhealth);

        StartCoroutine(FlashRed());

        if (currhealth <= 0)
        {

        }
    }

    private IEnumerator FlashRed()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriterenderer.color = Color.white;

    }


}
