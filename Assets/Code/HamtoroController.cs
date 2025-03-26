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
    public Transform aimPivot;
    public GameObject projectilePrefab;

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
            SoundManager.instance.PlaySoundJump();
            if (jumpsLeft > 0)
            {
                jumpsLeft--;
                // _rigidbody2D.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 10f); // Fixed jump velocity

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
