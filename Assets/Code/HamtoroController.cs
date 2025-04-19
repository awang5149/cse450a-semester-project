using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HamtoroController : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public int jumpsLeft;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    
    public EndScreen endScreen;

    private bool shieldActive;

    private bool ammoActive;
    // ammo system vars
    [SerializeField] private int currentAmmo = 15; // starting ammo amount
    [SerializeField] private int maxAmmoCapacity = 10; // max ammo player can hold
    [SerializeField] public int ammoReward = 1; // num bullets returned from kill

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
            /*
            if (CanShoot())
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
                ConsumeAmmo();
                // SoundManager.instance.PlaySoundFire(); // Add this if you have a fire sound
            }
            else
            {
                Debug.Log("Out of ammo!");
                SoundManager.instance.PlaySoundClick(); // Play clicking sound for empty ammo
                // need to stop player from being able to shoot if they are out of ammo
            }
            */
            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverUI())
                    return;
                if (!CanShoot()) // check if out of ammo
                {
                    Debug.Log("Out of ammo!");
                    SoundManager.instance.PlaySoundClick();
                    return;
                }
                Fire();
            }
        }
    }
    
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    public void SetShield(bool state)
    {
        shieldActive = state;
    }
    public void TakeHit()
    {
        if (shieldActive)
        {
            SetShield(false);
        }
        else
        {
            Die();
        }
    }

    public void SetAmmo(bool state)
    {
        ammoActive = state;
    }
    private void Fire()
    {
        var proj = Instantiate(projectilePrefab, transform.position, aimPivot.rotation);
        ConsumeAmmo();
        // SoundManager.instance.PlaySoundFire();
    }

    // Method to check if player has ammo to shoot
    public bool CanShoot()
    {
        return ammoActive || currentAmmo > 0;
    }

    // Method to consume ammo when shooting
    public void ConsumeAmmo()
    {
        if (ammoActive)
            return;
        if (currentAmmo > 0)
        {
            currentAmmo--;
            Debug.Log("Ammo used. Remaining ammo: " + currentAmmo);
        }
    }
    
    public void AddAmmo(int amount)
    {
        int actualAmount = amount * ammoReward; // apply ammo reward multiplier
        int newAmmo = Mathf.Min(currentAmmo + actualAmount, maxAmmoCapacity);
        
        // log only if ammo actually changes
        if (newAmmo != currentAmmo) {
            currentAmmo = newAmmo;
            Debug.Log("Ammo added. Current ammo: " + currentAmmo + "/" + maxAmmoCapacity);
        }
    }

    // FOR UPGRADE #1!!!
    public void IncreaseAmmoCapacity(int amount)
    {
        maxAmmoCapacity += amount;
        Debug.Log("Ammo capacity increased to: " + maxAmmoCapacity);
    }

    // getters for ui display
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmoCapacity()
    {
        return maxAmmoCapacity;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ice_block") || 
            collision.gameObject.CompareTag("ice_t_shape") ||
            collision.gameObject.CompareTag("ice_stair") ||
            collision.gameObject.CompareTag("ice_l_shape") ||
            collision.gameObject.CompareTag("ice_stacks") ||
            collision.gameObject.CompareTag("ice_u_shape") ) 
        {
            TerrainPooler.instance.ForceBiome("ice");
        }

        // add more biome tag checks here
        if (collision.gameObject.CompareTag("mud_block") ||
            collision.gameObject.CompareTag("mud_t_shape") ||
            collision.gameObject.CompareTag("mud_stair") ||
            collision.gameObject.CompareTag("mud_l_shape") ||
            collision.gameObject.CompareTag("mud_stacks") ||
            collision.gameObject.CompareTag("mud_u_shape"))
        {
            TerrainPooler.instance.ForceBiome("mud");
        }

        if (collision.gameObject.CompareTag("block") ||
            collision.gameObject.CompareTag("t_shape") ||
            collision.gameObject.CompareTag("stair") ||
            collision.gameObject.CompareTag("l_shape") ||
            collision.gameObject.CompareTag("stacks") ||
            collision.gameObject.CompareTag("u_shape"))
        {
            TerrainPooler.instance.ForceBiome("default");
        }

        if (collision.gameObject.CompareTag("stone_block") ||
            collision.gameObject.CompareTag("stone_t_shape") ||
            collision.gameObject.CompareTag("stone_stair") ||
            collision.gameObject.CompareTag("stone_l_shape") ||
            collision.gameObject.CompareTag("stone_stacks") ||
            collision.gameObject.CompareTag("stone_u_shape"))
        {
            TerrainPooler.instance.ForceBiome("stone");
        }
    }

    public void OnEnterBiome(string biome)
    {
        if (_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();

        if (biome == "ice")
        {
            _rigidbody2D.drag = 1f; // low drag = more slipping
            Debug.Log("Hamtoro is now on ICE � more slipping!");
        }
        else
        {
            _rigidbody2D.drag = 1f; // normal drag
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