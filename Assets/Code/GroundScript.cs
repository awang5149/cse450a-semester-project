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
        transform.Translate(Vector2.left * (speed * Time.deltaTime)); // simulate running by moving ground left
        
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
