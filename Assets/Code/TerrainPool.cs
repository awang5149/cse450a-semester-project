using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPool : MonoBehaviour
{
    public GameObject[] terrainPrefabs; //index 0 contains base ground block, otherwise contains structures
    public int poolSize = 15;

    private Queue<GameObject> groundPool = new Queue<GameObject>();

    void Start()
    {
        // Initialize the pool with terrain pieces
        for (int i = 0; i < poolSize; i++)
        {
            GameObject terrain = Instantiate(terrainPrefabs[0]);
            terrain.SetActive(false);
            groundPool.Enqueue(terrain);
        }
    }

    // Get terrain block from pool
    public GameObject GetTerrain()
    {
        if (groundPool.Count > 0)
        {
            GameObject terrain = groundPool.Dequeue();
            terrain.SetActive(true); // Activate ground block
            return terrain;
        }
        else
        {
            // If pool is empty, instantiate a new terrain
            GameObject terrain = Instantiate(terrainPrefabs[0]);
            terrain.SetActive(true);
            return terrain;
        }
    }

    // Return terrain block to pool
    public void ReturnTerrain(GameObject terrain)
    {
        terrain.SetActive(false);
        groundPool.Enqueue(terrain); 
    }
}