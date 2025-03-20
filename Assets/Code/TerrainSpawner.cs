using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{ // This class is based on this tutorial: https://www.youtube.com/watch?v=4MOEZW-ZjSQ
    private TerrainPooler terrainPooler;
    private string[] terrainNames = { "block", "u_shape" };
    private GameObject lastSpawnedBlock; // keep pointer to last spawned block so next block can be spawned right behind it

    public static TerrainSpawner instance;
    
    [SerializeField] private float groundSpawnDistance = 1.5f;
    [SerializeField] private float[] spawnProbabilities; // weighted probs so base block is most likely

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        terrainPooler = TerrainPooler.instance;
        
        lastSpawnedBlock = terrainPooler.SpawnFromPool("block", new Vector2(-5f, -4.87f));
        for (int i = 0; i < 9; i++)  // Spawn 9 more blocks (10 total)
        {
            SpawnBaseTerrain();
        }
    }

    // Update is called once per frame
    public void SpawnRandomTerrain()
    {
        string terrainName = GetRandomTerrainName();
        float lastBlockWidth = terrainPooler.GetBlockWidth(lastSpawnedBlock.name);
        float currentBlockWidth = terrainPooler.GetBlockWidth(terrainName);
        print("lastWidth: " + lastBlockWidth);
        print("currentWidth: " + currentBlockWidth);
        float lastBlockRightEdge = lastSpawnedBlock.transform.position.x + (lastBlockWidth / 2f);

        print("right edge: " + lastBlockRightEdge);
        float newBlockLeftEdge = lastBlockRightEdge;
        print("newLeft edge: " + newBlockLeftEdge);
        float spawnX = newBlockLeftEdge + (currentBlockWidth / 2f);
        
        Vector2 spawnPosition = new Vector2(spawnX, -4.87f);
        
        lastSpawnedBlock = terrainPooler.SpawnFromPool(terrainName, spawnPosition); //hardcoded approx start position (center later?)
    }

    public void SpawnBaseTerrain()
    {
        string terrainName = "block";
        float lastBlockWidth = terrainPooler.GetBlockWidth(lastSpawnedBlock.name);
        float currentBlockWidth = terrainPooler.GetBlockWidth(terrainName);
        
        float lastBlockRightEdge = lastSpawnedBlock.transform.position.x + (lastBlockWidth / 2f);
        // New block should have its left edge at the right edge of the last block
        float newBlockLeftEdge = lastBlockRightEdge;
        // Calculate center position of new block
        float spawnX = newBlockLeftEdge + (currentBlockWidth / 2f);
        
        Vector2 spawnPosition = new Vector2(spawnX, -4.87f);
        
        lastSpawnedBlock = terrainPooler.SpawnFromPool(terrainName, spawnPosition); //hardcoded approx start position (center later?)
    }

    private string GetRandomTerrainName()
    {
        float totalProb = 0;
        foreach (float prob in spawnProbabilities) {
            totalProb += prob;
        }

        float randomValue = Random.Range(0, totalProb);
        float cumulativeProb = 0;

        for (int i = 0; i < terrainNames.Length; i++)
        {
            cumulativeProb += spawnProbabilities[i];
            if (randomValue < cumulativeProb)
                return terrainNames[i];
        }

        return terrainNames[0];
    }
}
