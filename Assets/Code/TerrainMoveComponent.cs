using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMoveComponent : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float despawnDistance = -6.5f;
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector2.left * speed;
        if (transform.position.x <= despawnDistance)
        {
            TerrainSpawner.instance.SpawnRandomTerrain();

            gameObject.SetActive(false);
        }
    }
}
