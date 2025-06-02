using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BetterEnemySpawner : MonoBehaviour
{
    public enum ObjectType { Enemy }

    public Tilemap tilemap;
    public GameObject[] enemyPrefabs;
    public float enemyProbability = 1f;
    public int maxEnemy = 10;
    public float spawnInterval = 5f;

    private List<Vector3> validSpawnPositions = new List<Vector3>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool isSpawning = false;
    public string lootTag = "Loot";

    private void DestroyAllLoot()
    {
        GameObject[] lootObjects = GameObject.FindGameObjectsWithTag(lootTag);
        foreach (GameObject loot in lootObjects)
        {
            Destroy(loot);
        }
    }

    void Start()
    {
        GatherValidPositions();
        StartCoroutine(SpawnEnemiesIfNeeded());
        GameController.onreset += OnLevelChange;
    }

    void Update()
    {
        if (!tilemap.gameObject.activeInHierarchy)
        {
            OnLevelChange();
        }

        if (!isSpawning && ActiveEnemyCount() < maxEnemy)
        {
            StartCoroutine(SpawnEnemiesIfNeeded());
        }
    }

    private void OnLevelChange()
    {
        tilemap = GameObject.Find("MonsterNest").GetComponent<Tilemap>();
        GatherValidPositions();
        DestroyAllEnemies();
        DestroyAllLoot();
    }

    private int ActiveEnemyCount()
    {
        spawnedEnemies.RemoveAll(item => item == null);
        return spawnedEnemies.Count;
    }

    private IEnumerator SpawnEnemiesIfNeeded()
    {
        isSpawning = true;
        GatherValidPositions(); 

        while (ActiveEnemyCount() < maxEnemy)
        {
            TrySpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    private bool PositionHasObject(Vector3 positionToCheck)
    {
        return spawnedEnemies.Any(obj => obj && Vector3.Distance(obj.transform.position, positionToCheck) < 1.0f);
    }

    private ObjectType RandomObjectType()
    {
        return ObjectType.Enemy; 
    }

    private void TrySpawnEnemy()
    {
        if (validSpawnPositions.Count == 0) return;

        List<Vector3> shuffledPositions = validSpawnPositions.OrderBy(_ => Random.value).ToList();

        foreach (Vector3 potentialPos in shuffledPositions)
        {
            Vector3 left = potentialPos + Vector3.left;
            Vector3 right = potentialPos + Vector3.right;

            if (!PositionHasObject(left) && !PositionHasObject(right))
            {
                GameObject prefab = enemyPrefabs[(int)RandomObjectType()];
                GameObject enemy = Instantiate(prefab, potentialPos, Quaternion.identity);
                spawnedEnemies.Add(enemy);
                break; 
            }
        }
    }

    private void DestroyAllEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }

        spawnedEnemies.Clear();
    }

    private void GatherValidPositions()
    {
        validSpawnPositions.Clear();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        Vector3 start = tilemap.CellToWorld(new Vector3Int(bounds.xMin, bounds.yMin, 0));

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3 pos = start + new Vector3(x + 0.5f, y + 0.5f, 0);
                    validSpawnPositions.Add(pos);
                }
            }
        }
    }

}
