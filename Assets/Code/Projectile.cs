using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Projectile : MonoBehaviour
    {
        // Outlets
        Rigidbody2D _rigidbody2D;

        // Methods
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = transform.right * 10f;
        }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Eagle")
        {
            SoundManager.instance.PlaySoundHit();
        }
        if (other.gameObject.tag == "block" || other.gameObject.tag == "t_shape" || other.gameObject.tag == "stair" || other.gameObject.tag=="l_shape" || other.gameObject.tag == "u_shape")
            {
                SoundManager.instance.PlaySoundMiss();
            }
            if (other.gameObject.GetComponent<EagleScript>() != null)
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Eagle")) 
            {
                Destroy(collision.gameObject); // Destroy the eagle
            }
            Destroy(gameObject); // Destroy the projectile after impact
        }

        void OnBecameInvisible()
        {
            Destroy(this.gameObject);
        }
    }

