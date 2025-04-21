using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Outlets
    Rigidbody2D _rigidbody2D;
    // Ensure ammo is awarded only once per projectile hit
    private bool ammoAwarded = false;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * 10f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // If we hit an eagle and haven't already awarded the bonus ammo:
        if (!ammoAwarded && other.gameObject.CompareTag("Eagle"))
        {
            SoundManager.instance.PlaySoundHit();
        
            // Award 1 ammo by accessing HamtoroController
            HamtoroController hamtoro = FindObjectOfType<HamtoroController>();
            if (hamtoro != null)
            {
                hamtoro.AddAmmo(1);
            }
            ammoAwarded = true;
            ScoreAndMoneyManager.instance.score += 50;
            Destroy(other.gameObject);
        }
        
        // Check for blocks or other shapes to play miss sound
        if (other.gameObject.CompareTag("block") ||
            other.gameObject.CompareTag("t_shape") ||
            other.gameObject.CompareTag("stair") ||
            other.gameObject.CompareTag("l_shape") ||
            other.gameObject.CompareTag("u_shape"))
        {
            SoundManager.instance.PlaySoundMiss();
        }
        
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
