using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinSpawner : MonoBehaviour
{
    public enum ObjectType { Coin }

    public Tilemap tilemap;
    public GameObject[] coinPrefab;
    public float coinProbability = 0.1f;
    public int maxCoin = 5;
    public float spawnInterval = 0.5f;

    private List<Vector3> validSpawnPositions = new List<Vector3>();
    private List<GameObject> spawnedCoins = new List<GameObject>();
    private bool isSpawning = false;

    void Start()
    {
        GatherValidPositions();
        StartCoroutine(SpawnCoinsIfNeeded());
        GameController.onreset += LevelChange;
    }

    void Update()
    {
        if (!tilemap.gameObject.activeInHierarchy)
        {
            LevelChange();
        }
        if (!isSpawning && ActiveObjectCount() < maxCoin)
        {
            StartCoroutine(SpawnCoinsIfNeeded());
        }
    }

    private void LevelChange()
    {
        tilemap = GameObject.Find("CoinLocation").GetComponent<Tilemap>();
        GatherValidPositions();
        DestroyAllCoins();
    }

    private int ActiveObjectCount()
    {
        spawnedCoins.RemoveAll(item => item == null);
        return spawnedCoins.Count;
    }

    private IEnumerator SpawnCoinsIfNeeded()
    {
        isSpawning = true;
        while (ActiveObjectCount() < maxCoin)
        {
            SpawnCoins();
            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }

    private bool PositionHasObject(Vector3 positionToCheck)
    {
        return spawnedCoins.Any(obj => obj && Vector3.Distance(obj.transform.position, positionToCheck) < 1.0f);
    }

    private ObjectType RandomObjectType()
    {
        float randomChoice = Random.value;
        if (randomChoice <= coinProbability)
        {
            return ObjectType.Coin;
        }
        else if (randomChoice <= coinProbability)
        {
            return ObjectType.Coin;
        }
        else
        {
            return ObjectType.Coin;
        }
    }

    private void SpawnCoins()
    {
        if (validSpawnPositions.Count == 0)
        {
            return;
        }

        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;

        while (!validPositionFound && validSpawnPositions.Count > 0)
        {
            int randomIndex = Random.Range(0, validSpawnPositions.Count);
            Vector3 potentialPosition = validSpawnPositions[randomIndex];
            Vector3 leftPosition = potentialPosition + Vector3.left;
            Vector3 rightPosition = potentialPosition + Vector3.right;

            if (!PositionHasObject(leftPosition) && !PositionHasObject(rightPosition))
            {
                spawnPosition = potentialPosition;
                validPositionFound = true;
            }
            validSpawnPositions.RemoveAt(randomIndex);
        }

        if (validPositionFound)
        {
            ObjectType objectType = RandomObjectType();
            GameObject gameObject = Instantiate(coinPrefab[(int)objectType], spawnPosition, Quaternion.identity);
            spawnedCoins.Add(gameObject);
        }
    }

    private void DestroyAllCoins()
    {
        foreach (GameObject obj in spawnedCoins)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedCoins.Clear();
    }

    private void GatherValidPositions()
    {
        validSpawnPositions.Clear();
        BoundsInt boundsInt = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(boundsInt);
        Vector3 start = tilemap.CellToWorld(new Vector3Int(boundsInt.xMin, boundsInt.yMin, 0));

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = allTiles[x + y * boundsInt.size.x];
                if (tile != null)
                {
                    Vector3 place = start + new Vector3(x + 0.5f, y + 2f, 0);
                    validSpawnPositions.Add(place);
                }
            }
        }
    }
}
