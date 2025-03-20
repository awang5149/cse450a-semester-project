using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public float speed;
    private float originalSpeed;
    private bool isBoosted = false;
    
    void Start()
    {
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);// simulate running by moving ground left
        
        if (Input.GetKeyDown(KeyCode.D) && !isBoosted)
        {
            StartCoroutine(SpeedBoost());
        }
    }
    
    IEnumerator SpeedBoost()
    {
        isBoosted = true;
        speed *= 2f;
        yield return new WaitForSeconds(1f);
        speed = originalSpeed;
        isBoosted = false;
    }
}
