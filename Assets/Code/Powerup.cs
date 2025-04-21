using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public bool vacuum;
    public bool shield;
    public bool ammo;
    public int duration = 5;

    public float amplitude = 1f;
    public float frequency = 1f;
    public Vector3 floatAxis = Vector3.up;

    public float speed = 5f;

    private Vector3 startPos;
    private float elapsed;
    
    PowerupManager powerupManager;
    

    void Start()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
        startPos = transform.position;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        
        float yOffset = Mathf.Sin(elapsed * frequency * 2f * Mathf.PI) * amplitude;
        
        float xOffset = -speed * elapsed;

        // set final position
        transform.position = startPos + new Vector3(xOffset, yOffset, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        powerupManager.ActivatePowerup(vacuum, shield, ammo, duration);
        Destroy(gameObject);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
