
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehavior : MonoBehaviour
{
    public float slideSpeed = 7f; 
    public float speed = 10f;

    private Rigidbody2D rb;

    private bool hasTarget;

    private Vector3 targetPosition;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        // move seed left toward hamtaro
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized; 
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * slideSpeed;
        }
    }

    public void setTarget(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        hasTarget = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreAndMoneyManager.instance.AddMoney(1 * ScoreAndMoneyManager.instance.seedMultiplier); // increase seed counter
            Debug.Log("Seeds Collected: " + ScoreAndMoneyManager.instance.money);
            SoundManager.instance.PlaySoundSeedCollected();
            Destroy(this.gameObject); // remove seed from game
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    
}