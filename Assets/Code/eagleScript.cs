using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EagleScript : MonoBehaviour
{
    
    public EagleGenerator EagleGenerator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.left * (EagleGenerator.currentSpeed * Time.deltaTime)); // send eagles at player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spawn"))
        {
            EagleGenerator.generateEagle(); // if eagle passes through spawn trigger, spawn another
        }
        
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(this.gameObject); // if eagle passes through destroy trigger, destroy this instance
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.GetComponent<HamtoroController>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene only when colliding with player
        }
    }
}
