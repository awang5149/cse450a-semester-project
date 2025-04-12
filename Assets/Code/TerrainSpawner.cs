using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{ // This class is based on this tutorial: https://www.youtube.com/watch?v=4MOEZW-ZjSQ
    private TerrainPooler terrainPooler;
    private string[] terrainNames = { "block", "t_shape", "stair", "l_shape", "stacks", "u_shape" };
    private GameObject lastSpawnedBlock; // keep pointer to last spawned block so next block can be spawned right behind it

    public static TerrainSpawner instance;
    
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
        for (int i = 0; i < 12; i++)  // Spawn 11 more blocks (12 total)
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
        float lastBlockRightEdge = lastSpawnedBlock.transform.position.x + (lastBlockWidth / 2f);
        
        float newBlockLeftEdge = lastBlockRightEdge;

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
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class TerrainSpawner : MonoBehaviour
// {
//     private TerrainPooler terrainPooler;
//     private GameObject lastSpawnedBlock;
//
//     public static TerrainSpawner instance;
//     private string currentBiome = "default";
//
//     private Dictionary<string, string[]> biomeTerrains = new Dictionary<string, string[]>()
//     {
//         { "default", new string[] { "block", "t_shape", "stair", "l_shape", "stacks", "u_shape" } },
//         { "ice", new string[] { "ice_block", "ice_t_shape", "ice_stair", "ice_l_shape" } },
//         { "mud", new string[] { "mud_block", "mud_t_shape", "mud_stair", "mud_l_shape" } },
//         { "stone", new string[] { "stone_block", "stone_t_shape", "stone_stair", "stone_l_shape" } }
//     };
//
//     void Awake()
//     {
//         instance = this;
//     }
//
//     void Start()
//     {
//         terrainPooler = TerrainPooler.instance;
//         lastSpawnedBlock = terrainPooler.SpawnFromPoolByBiome("default", new Vector2(-5f, -4.87f));
//
//         for (int i = 0; i < 12; i++)
//         {
//             SpawnBaseTerrain();
//         }
//     }
//
//     void Update()
//     {
//         UpdateBiome();
//     }
//
//     private void UpdateBiome()
//     {
//         int score = ScoreAndMoneyManager.instance.score;
//
//         if (score >= 3000)
//             currentBiome = "stone";
//         else if (score >= 2000)
//             currentBiome = "mud";
//         else if (score >= 1000)
//             currentBiome = "ice";
//         else
//             currentBiome = "default";
//     }
//
//     public void SpawnRandomTerrain()
//     {
//         float lastBlockWidth = terrainPooler.GetBlockWidth(lastSpawnedBlock.name);
//         float lastBlockRightEdge = lastSpawnedBlock.transform.position.x + (lastBlockWidth / 2f);
//
//         Vector2 spawnPosition = new Vector2(lastBlockRightEdge, -4.87f);
//         lastSpawnedBlock = terrainPooler.SpawnFromPoolByBiome(currentBiome, spawnPosition);
//     }
//
//     public void SpawnBaseTerrain()
//     {
//         float lastBlockWidth = terrainPooler.GetBlockWidth(lastSpawnedBlock.name);
//         float lastBlockRightEdge = lastSpawnedBlock.transform.position.x + (lastBlockWidth / 2f);
//
//         Vector2 spawnPosition = new Vector2(lastBlockRightEdge, -4.87f);
//         lastSpawnedBlock = terrainPooler.SpawnFromPoolByBiome("default", spawnPosition);
//     }
// }
