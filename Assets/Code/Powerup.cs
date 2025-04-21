using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public bool vacuum;
    public bool shield;
    public bool ammo;
    public float duration = 5f;

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

    // THIS IS FOR UPGRADE#3 TO UPGRADE POWER UP DURATION, CALLED ONCE PER PURCHASE
    public void UpdatePowerUpDuration(){
        Debug.Log("inside UpdatePowerUpDuration(). old duration : " + duration);
        duration ++;
        Debug.Log("new duration after updating: " + duration);
    }
}
