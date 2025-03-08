using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGenerator : MonoBehaviour
{
    public GameObject seedPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2f; 

    void Start()
    {
        StartCoroutine(SpawnSeeds());

    }

    IEnumerator SpawnSeeds()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(seedPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
