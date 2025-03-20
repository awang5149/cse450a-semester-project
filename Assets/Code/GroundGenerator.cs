using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject[] groundPrefabs; // index 0 contains base ground block, otherwise contains other terrains
	public float[] spawnProbs;
	public float groundSpawnRate = 0.5f; // 10% spawn probability

    public int groundCount;
    private float groundWidth;
    private Dictionary<string, float> prefabWidths = new Dictionary<string, float>();
    private Queue<GameObject> groundPool = new Queue<GameObject>();
    private GameObject lastGround; // pointer to access last added ground item
    private Vector2 startPosition;
    private GameObject hamster;
    
    // Start is called before the first frame update
    private void Start()
    {
        foreach (GameObject g in groundPrefabs)
        {
            float width = g.GetComponentInChildren<SpriteRenderer>().GetComponent<SpriteRenderer>().bounds.size.x;
            prefabWidths[g.name] = width;
        }
        
        groundWidth = prefabWidths[groundPrefabs[0].name];
        startPosition = transform.position;
        print(startPosition);
        hamster = GameObject.FindWithTag("Player");
        
        for (int i = groundCount - 1; i >= 0; i--)
        {
            // calculate position of the ground piece, instantiate, and add to queue
            float calculatedPositionX = -i * groundWidth + 12f;

            Vector2 calculatedPosition = new Vector2(calculatedPositionX, startPosition.y);
            
            GameObject ground = Instantiate(groundPrefabs[0], calculatedPosition, Quaternion.identity);
            groundPool.Enqueue(ground);
            lastGround = ground;
        }
    }

    // Update is called once per frame
    void Update()
    {
		float playerPos = hamster.transform.position.x;
        GameObject firstGround = groundPool.Peek();
        float firstGroundPos = firstGround.transform.position.x;

         if (firstGroundPos < playerPos - (groundWidth + 2f)) 
         { // if first ground is past player, move ground to end of queue
             groundPool.Dequeue();
             float lastGroundPos = lastGround.transform.position.x;
             
             if(Random.value < groundSpawnRate) 
             { // if value is less than groundSpawnRate, generate and add a random groundPrefab
                 GameObject newTerrainPrefab = GetRandomTerrain();
                 float prefabWidth = prefabWidths[newTerrainPrefab.name];

                 firstGround.transform.position = new Vector2(lastGroundPos + prefabWidth, startPosition.y); //update position of newly spawned ground to be after prefab
                 lastGround = firstGround;

                 Vector2 newPosition = new Vector2(firstGround.transform.position.x, startPosition.y + 1.6f); //position of new block

                 Instantiate(newTerrainPrefab, newPosition, Quaternion.identity);
             }
             else
             {
                 firstGround.transform.position = new Vector2(lastGroundPos + groundWidth, startPosition.y);
                 groundPool.Enqueue(firstGround);
                 lastGround = firstGround;
             }
         }
    }
	private GameObject GetRandomTerrain()
    { // based on probability weights in spawnProbs[], gets a random terrain in groundPrefabs[]
        float totalProb = 0;
        foreach (float prob in spawnProbs) {
			totalProb += prob;
		}

        float randomValue = Random.Range(0, totalProb);
        float cumulativeProb = 0;

        for (int i = 0; i < groundPrefabs.Length; i++)
        {
            cumulativeProb += spawnProbs[i];
            if (randomValue < cumulativeProb)
                return groundPrefabs[i];
        }

        return groundPrefabs[0];
    }
}
