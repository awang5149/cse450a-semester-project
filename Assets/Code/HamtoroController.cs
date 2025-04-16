using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HamtoroController : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public int jumpsLeft;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    
    public EndScreen endScreen;

    public int ammoCount = 3;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Awake(){
        endScreen = FindObjectOfType<EndScreen>(true); // get the EndScreen component to be able to refer to its gameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseAndResume.gameIsPaused)
            return;

        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.velocity = new Vector2(1f, _rigidbody2D.velocity.y);
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsLeft > 0)
            {
                SoundManager.instance.PlaySoundJump();

                jumpsLeft--;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 10.5f); // Fixed jump velocity
            }
        }

        _animator.SetInteger("jumpsLeft", jumpsLeft);

        // aim toward mouse for shooting
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        // Shoot
        if (Input.GetMouseButtonDown(0))
        {
            if (ammoCount > 0)
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
                ammoCount--;
            }
            else
            {
                Debug.Log("Shot limit reached!");
                SoundManager.instance.PlaySoundClick(); // Play clicking sound
            }
        }

    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount;
        Debug.Log("Ammo added. Current ammo: " + ammoCount);
    }

    void OnCollisionStay2D(Collision2D other){
        float detectionRadius = 0.5f;
        Vector2 checkPosition = transform.position + Vector3.down * 0.1f;

        Collider2D hit = Physics2D.OverlapCircle(checkPosition, detectionRadius, LayerMask.GetMask("Ground"));

        if (hit != null)
        {
            jumpsLeft = 2;
        }
    }
    
    private void OnBecameInvisible()
    {
        if (!ScoreAndMoneyManager.instance.isAlive) return;
        Die();
    }

    // death 
    public void Die()
    {
        if (!ScoreAndMoneyManager.instance.isAlive) return; 
        // update high score and total currency BEFORE resetting and bEFORE displaying end screen so its updated properly!!
        GameController.instance.UpdateHighScoreAndTotalCurrency(); 
        ScoreAndMoneyManager.instance.SetPlayerDead(); // set player as dead; just a setter method to set isAlive variable to false
        
        // show end screen
        if (endScreen != null)
        {
            endScreen.Show(ScoreAndMoneyManager.instance.score);
        }
        else
        {
            Debug.LogError("EndScreen instance not found!");
        }

        // Optional: Disable player controls
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.enabled = false;
    }

}
