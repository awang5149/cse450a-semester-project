using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HamtoroController : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    public int jumpsLeft;
    public Transform aimPivot;
    public GameObject projectilePrefab;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseAndResume.gameIsPaused)
            return;
        
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.velocity = new Vector2(1f, _rigidbody2D.velocity.y);
        }
        // Jump
        if (Input.GetKeyDown(KeyCode.Space)){
            if (jumpsLeft > 0)
            {
                SoundManager.instance.PlaySoundJump();

                jumpsLeft--;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 10.5f); // Fixed jump velocity
            }
        }

        // aim toward mouse for shooting
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x); 
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        // Shoot
        if(Input.GetMouseButtonDown(0)){
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /*
    // this code should reset scene when hamtaro hits an obstacle but not working rn
    void onTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<HamtoroController>()){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    */
}
