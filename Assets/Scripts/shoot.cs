using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bulletprefab;
    public float bulletspeed = 50f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }

    void Shooting()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootdirection = (mousePosition - transform.position).normalized;

        GameObject bullet = Instantiate(bulletprefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootdirection.x, shootdirection.y) * bulletspeed;
        Destroy(bullet, 2f);
    }
}
