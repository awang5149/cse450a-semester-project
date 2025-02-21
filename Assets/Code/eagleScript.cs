using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eagleScript : MonoBehaviour
{
    
    public eagleGenerator eagleGenerator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * (eagleGenerator.currentSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NextLine"))
        {
            eagleGenerator.generateEagle();
        }
        
        if (collision.gameObject.CompareTag("Endpoint"))
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //Reload scene only when colliding with player
        if (other.gameObject.GetComponent<HamtoroController>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
