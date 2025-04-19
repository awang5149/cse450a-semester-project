using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public List<GameObject> powerups;
    
    public float minTime = 3f, maxTime = 3f;
    public float spawnX = 14f, spawnY = 1f;
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(wait);

            // pick one at random
            GameObject prefab = powerups[Random.Range(0, powerups.Count)];
            Instantiate(prefab, new Vector2(spawnX, spawnY), Quaternion.identity);
        }
    }
}
