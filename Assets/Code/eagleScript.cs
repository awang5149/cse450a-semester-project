using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EagleScript : MonoBehaviour
{
    
    public EagleGenerator EagleGenerator;
    public EndScreen endScreen;

    private SpriteRenderer spriteRenderer;

    // eagle homing
    public Transform player;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (endScreen == null)
        {
            endScreen = FindObjectOfType<EndScreen>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
         if (player != null)
         {
             Vector2 direction = (player.position - transform.position).normalized;

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = direction.x > 0; 
            }

            transform.position += (Vector3)direction * EagleGenerator.currentSpeed * Time.deltaTime;
            
             float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
             Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
             transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
         }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<HamtoroController>();
            if (player != null)
            {
                player.TakeHit(); // if eagle collides w hamtaro, take hit
                Destroy(this.gameObject);
            }
        }
    }

    // eagle dies
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}