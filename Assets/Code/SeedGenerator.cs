using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGenerator : MonoBehaviour
{
    public GameObject seedPrefab; 
    public Transform[] spawnPoints; 
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 1.5f; 
    public float randomRadius = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnSeeds());

    }

    IEnumerator SpawnSeeds()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
            
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            Vector2 randomOffset = Random.insideUnitCircle * randomRadius;
            Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, 0, randomOffset.y);
            
            Instantiate(seedPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
