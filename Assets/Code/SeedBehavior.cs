
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehavior : MonoBehaviour
{
    public float slideSpeed = 5f; 
    public float speed = 10f; 
    public static int seedCount = 0;
    //private bool slidingIn = true; // track if seed is moving onto the screen

    void Update()
    {
        // move seed left toward hamtaro
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            seedCount++; // increase seed counter
            Debug.Log("Seeds Collected: " + seedCount);
            SoundManager.instance.PlaySoundSeedCollected();
            Destroy(this.gameObject); // remove seed from game

        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public static int GetSeedCount()
    {
        return seedCount; 
    }
}