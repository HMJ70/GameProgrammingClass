using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject[] fruitPrefabs; 
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

        Vector3 shootDirection = (mousePosition - transform.position).normalized;
        Vector3 spawnOffset = shootDirection * 1f;
        Vector3 spawnPosition = transform.position + spawnOffset;

        int randomIndex = Random.Range(0, fruitPrefabs.Length);
        GameObject randomFruit = fruitPrefabs[randomIndex];

        GameObject bullet = Instantiate(randomFruit, spawnPosition, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletspeed;

        Destroy(bullet, 1f);
    }
}
