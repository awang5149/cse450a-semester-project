using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HamtoroController : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    public float moveSpeed = 5f;
    private bool isGrounded = false;
    
    public int jumpsLeft;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // Horizontal Movement
        float moveInput = Input.GetAxis("Horizontal");
        if (isGrounded)
        {
            _rigidbody2D.velocity = new Vector2(moveInput * moveSpeed, _rigidbody2D.velocity.y);
        }
        // Jump
        if (Input.GetKeyDown(KeyCode.Space)){
            if (jumpsLeft > 0)
            {
                jumpsLeft--;
                _rigidbody2D.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
            }
        }

    }

    void OnCollisionStay2D(Collision2D other){
        // Check that we collided w ground
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.7f);

            for(int i=0; i<hits.Length; i++){
                RaycastHit2D hit = hits[i];
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = 2;
                }
            }
        }
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
