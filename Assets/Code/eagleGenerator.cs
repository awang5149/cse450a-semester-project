using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleGenerator : MonoBehaviour
{
    public GameObject eagle;

    public Transform[] spawnPoints; // Array of 3 spawn point transforms
    private int currentSpawnIndex = 0;

    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;
    public float spawnRate = 7.0f;
    public float speedConstant;

    void Awake()
    {
        currentSpeed = minSpeed;
        InvokeRepeating(nameof(generateEagle), 1f, spawnRate);
    }

    public void generateEagle()
    {
        // Use current spawn point
        Transform spawn = spawnPoints[currentSpawnIndex];
        Debug.Log(currentSpawnIndex);
        GameObject newEagle = Instantiate(eagle, spawn.position, spawn.rotation);
        newEagle.GetComponent<EagleScript>().EagleGenerator = this;

        // Update spawn index to cycle through points
        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Length;
        
    }

    void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            // currentSpeed += speedConstant;
        }
    }
}
