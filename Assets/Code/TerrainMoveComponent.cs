using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMoveComponent : MonoBehaviour
{ // This class is based on this tutorial: https://www.youtube.com/watch?v=4MOEZW-ZjSQ
    [SerializeField] private float speed = 2f;
    [SerializeField] private float despawnDistance = -8f;
    private Rigidbody2D _rb;

    private bool canSpawnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector2.left * speed;

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
