using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public TerrainPool terrainPool; // Reference to the TerrainPool script
    public float groundSpeed = 2f; // Speed at which ground moves
    public float terrainSpawnRate = 0.3f; // Probability of spawning terrain
    public float terrainDistance = 10f; // Distance between terrain pieces

    private Transform player;
    private float lastTerrainPositionX;
    private GameObject lastTerrain;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        lastTerrainPositionX = 0f;

        // Initialize the first terrain piece
        SpawnTerrain();
    }

    void Update()
    {
        lastTerrain.transform.Translate(Vector2.left * groundSpeed * Time.deltaTime);
        
        if (lastTerrain.transform.position.x + terrainDistance < player.position.x)
        {
            SpawnTerrain();
        }
    }

    // Spawn a new terrain piece
    void SpawnTerrain()
    {
        // Get the next terrain piece from the pool
        GameObject terrain = terrainPool.GetTerrain();

        // Randomly pick a terrain prefab
        GameObject randomTerrain = terrainPool.terrainPrefabs[Random.Range(1, terrainPool.terrainPrefabs.Length)];

        // Set the position of the new terrain piece
        terrain.transform.position = new Vector2(lastTerrainPositionX + terrainDistance, 0f);
        lastTerrainPositionX = terrain.transform.position.x;

        // Set the terrain type
        terrain.GetComponent<SpriteRenderer>().sprite = randomTerrain.GetComponent<SpriteRenderer>().sprite;

        // Return the old terrain piece to the pool if it's offscreen
        terrainPool.ReturnTerrain(lastTerrain);

        // Update lastTerrain to the new terrain
        lastTerrain = terrain;
    }
}
