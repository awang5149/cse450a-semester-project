using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMoveComponent : MonoBehaviour
{ // This class is based on this tutorial: https://www.youtube.com/watch?v=4MOEZW-ZjSQ
    [SerializeField] private float speed = 5f;

    [SerializeField] private float despawnDistance = -12f;

    private bool canSpawnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= despawnDistance && canSpawnGround)
        {
            TerrainSpawner.instance.SpawnRandomTerrain();
            canSpawnGround = false;
        }
        if(transform.position.x <= despawnDistance)
        {
            canSpawnGround = true;
            gameObject.SetActive(false);
        }
    }
}
