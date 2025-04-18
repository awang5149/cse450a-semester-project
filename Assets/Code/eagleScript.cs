using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EagleScript : MonoBehaviour
{
    
    public EagleGenerator EagleGenerator;
    public EndScreen endScreen;


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
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.left * (EagleGenerator.currentSpeed * Time.deltaTime)); // send eagles at player
         if (player != null)
         {
             Vector2 direction = (player.position - transform.position).normalized;
             
             transform.position += (Vector3)direction * speed * Time.deltaTime;
             
             float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
             Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
             transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
         }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<HamtoroController>();
        if (player != null)
        {
            player.TakeHit(); // if eagle collides w hamtaro, die
        }
    }

    // eagle dies
    void OnBecameInvisible()
    {
        ScoreAndMoneyManager.instance.score += 50;
        Destroy(this.gameObject);
    }
}