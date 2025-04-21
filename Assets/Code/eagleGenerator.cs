using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleGenerator : MonoBehaviour
{
    public GameObject eagle;

    public Transform[] spawnPoints;
    private int currentSpawnIndex = 0;

    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;
    
    // Spawn timing variables
    public float initialTimeBetweenSpawns = 7.0f;
    public float minimumTimeBetweenSpawns = 2.0f;  
    public float decreaseAmount = 0.1f;       
    private float currentTimeBetweenSpawns;       
    
    public float speedConstant;
    private float nextSpawnTime;                

    void Awake()
    {
        currentSpeed = minSpeed;
        currentTimeBetweenSpawns = initialTimeBetweenSpawns;
        nextSpawnTime = Time.time + 1f;        
    }

    void Update()
    {
        // Check if it's time to spawn an eagle
        if (Time.time >= nextSpawnTime)
        {
            generateEagle();
            nextSpawnTime = Time.time + currentTimeBetweenSpawns;
            
            // Decrease time between spawns to make them more frequent
            if (currentTimeBetweenSpawns > minimumTimeBetweenSpawns)
            {
                currentTimeBetweenSpawns -= decreaseAmount;
                // Ensure we don't go below minimum time
                currentTimeBetweenSpawns = Mathf.Max(currentTimeBetweenSpawns, minimumTimeBetweenSpawns);
            }
        }
        
        if (currentSpeed < maxSpeed)
        {
            // currentSpeed += speedConstant;
        }
    }

    public void generateEagle()
    {
        // Use current spawn point
        Transform spawn = spawnPoints[currentSpawnIndex];
        Debug.Log(currentSpawnIndex);
        GameObject newEagle = Instantiate(eagle, spawn.position, spawn.rotation);
        newEagle.GetComponent<EagleScript>().EagleGenerator = this;

        // Update spawn index to cycle through points
        currentSpawnIndex = Random.Range(0, spawnPoints.Length);
    }
}