using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bulletprefab;
    public float bulletspeed = 50f;
    public float shootCooldown = 1f;

    private float lastShootTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShootTime + shootCooldown)
        {
            Shooting();
            lastShootTime = Time.time;
        }
    }

    void Shooting()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 

        Vector3 shootdirection = (mousePosition - transform.position).normalized;

        GameObject bullet = Instantiate(bulletprefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootdirection * bulletspeed;
        Destroy(bullet, 1f);
    }
}
