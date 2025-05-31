using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BetterEnemySpawner : MonoBehaviour
{
    public enum ObjectType {Enemy}

    public Tilemap tilemap;
    public GameObject[] enemyprefab;
    public float enemyprobability = 0.1f;
    public int maxenemy = 5;
    public float spawninterval = 0.5f;

    private List<Vector3> validspawnpositions = new List<Vector3>();
    private List<GameObject> spawnenemy = new List<GameObject>();
    private bool isspawning = false;


    void Start()
    {
        gathervalidpositions();
        StartCoroutine(spawnenemyifneeded());
        GameController.onreset += levelchange;

    }

    void Update()
    {
        if(!tilemap.gameObject.activeInHierarchy)
        {
            levelchange();
        }
        if(!isspawning && activeobjectcount() < maxenemy)
        {
            StartCoroutine(spawnenemyifneeded());
        }

    }


    private void levelchange()
    {
        tilemap = GameObject.Find("MonsterNest").GetComponent<Tilemap>();
        gathervalidpositions();
        destroyallenemies();
    }

    

    private int activeobjectcount()
    {
        spawnenemy.RemoveAll(item => item == null);
        return spawnenemy.Count;
    }

    private IEnumerator spawnenemyifneeded()
    {
        isspawning = true;
        while(activeobjectcount() < maxenemy)
        {
            spawnenemies();
            yield return new WaitForSeconds(spawninterval);
        }
        isspawning=false;
    }

    private bool positionhasobject(Vector3 positiontocheck)
    {
        return spawnenemy.Any(checkobj => checkobj && Vector3.Distance(checkobj.transform.position,positiontocheck) < 1.0f);
    }

    private ObjectType RandomObjectType()
    {
        float randomChoice = Random.value;
        if(randomChoice <= enemyprobability)
        {
            return ObjectType.Enemy;
        }
        else if(randomChoice <= enemyprobability)
        {
            return ObjectType.Enemy;
        }
        else
        {
            return ObjectType.Enemy;
        }
        
    }    

    private void spawnenemies()
    {
        if(validspawnpositions.Count == 0)
        {
            return;
        }
        Vector3 spawnPosition = Vector3.zero;
        bool validpositionfound = false;

        while(!validpositionfound && validspawnpositions.Count > 0)
        {
            int randomIndex = Random.Range(0, validspawnpositions.Count);
            Vector3 potentialposition = validspawnpositions[randomIndex];
            Vector3 leftposition = potentialposition + Vector3.left;
            Vector3 rightposition = potentialposition + Vector3.right;

            if(!positionhasobject(leftposition) && !positionhasobject(rightposition))
            {
                spawnPosition = potentialposition;
                validpositionfound = true;
            }
            validspawnpositions.RemoveAt(randomIndex);
        }
        if (validpositionfound)
        {
            ObjectType objectType = RandomObjectType();
            //GameObject chosenprefab = enemyprefab[(int) objectType];
            GameObject gameObject = Instantiate(enemyprefab[(int)objectType], spawnPosition, Quaternion.identity);
            spawnenemy.Add(gameObject);


        }
    }

    private void destroyallenemies()
    {
        foreach (GameObject obj in spawnenemy)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnenemy.Clear();
    }



        private void gathervalidpositions()
        {
        validspawnpositions.Clear();
        BoundsInt boundsInt = tilemap.cellBounds;
        TileBase[] alltiles = tilemap.GetTilesBlock(boundsInt);
        Vector3 start = tilemap.CellToWorld(new Vector3Int(boundsInt.xMin, boundsInt.yMin, 0));
        
        for(int x = 0; x < boundsInt.size.x; x++)
        {
            for(int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = alltiles[x + y * boundsInt.size.x];
                if(tile != null)
                {
                    Vector3 place = start + new Vector3(x + 0.5f, y + 2f,0);
                    validspawnpositions.Add(place);
                }
            }
        }

    }
}
