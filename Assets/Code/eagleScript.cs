using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EagleScript : MonoBehaviour
{
    
    public EagleGenerator EagleGenerator;
    public EndScreen endScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        if (endScreen == null)
        {
            endScreen = FindObjectOfType<EndScreen>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * (EagleGenerator.currentSpeed * Time.deltaTime)); // send eagles at player
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.GetComponent<HamtoroController>())
        {
            int seedCount = SeedBehavior.GetSeedCount();
            
            if (endScreen != null)
            {
                Time.timeScale = 0f;
                endScreen.Show(seedCount);
            }
            else 
            {            
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene only when colliding with player
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
