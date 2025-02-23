using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform player; 
    public Transform[] spawnLocations; 
    public int enemiesPerWave = 3; 
    public float spawnTime = 2f; 
    public float minDistanceFromPlayer = 5f; 
    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        int spawned = 0;
        List<int> availableLocations = new List<int>();
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            availableLocations.Add(i);
        }

        while (spawned < enemiesPerWave && availableLocations.Count > 0)
        {
            int randIndex = Random.Range(0, availableLocations.Count);
            Transform spawnPoint = spawnLocations[availableLocations[randIndex]];

            if (Vector3.Distance(spawnPoint.position, player.position) >= minDistanceFromPlayer)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                spawned++;
            }
            availableLocations.RemoveAt(randIndex);
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}
