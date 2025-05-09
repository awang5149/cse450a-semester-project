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
    
    [SerializeField] public EndScreen endScreen;
    public Powerup powerup;

    private bool shieldActive;
    private bool vacuumActive;
    private bool ammoActive;
    
    // ammo system vars
    private int maxAmmoCapacity = 5; // max ammo player can hold
    private int currentAmmo; // starting ammo amount
    public int ammoReward = 1; // num bullets returned from kill

    private PauseAndResume pauser;

    void Awake()
    {
        if (endScreen == null)
        {
            endScreen = FindObjectOfType<EndScreen>(true); // `true` includes inactive objects
            if (endScreen == null)
                Debug.LogError("EndScreen not found in scene!");
        }
    }
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        //GameController.instance.UpdateRemainingAmmoUI();
        if (GameController.instance != null){
            GameController.instance.UpdateRemainingAmmoUI();
        }
        pauser = FindObjectOfType<PauseAndResume>();
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
                _animator.SetTrigger("Jump");
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
        if (Input.GetMouseButtonDown(0))
        {
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
        _animator.SetBool("isShielded", state);
    }
    public void SetVacuum(bool state)
    {
        vacuumActive = state;
        _animator.SetBool("isVacuuming", state);
    }
    public void SetAmmo(bool state) //horrible name but i dont wanna change it sorry guys!
    {
        ammoActive = state;
        _animator.SetBool("isAmmo", state);
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
    
    private void Fire()
    {
        var proj = Instantiate(projectilePrefab, transform.position, aimPivot.rotation);
        ConsumeAmmo();
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
        {
            return;
        }
            
        if (currentAmmo > 0)
        {
            currentAmmo--;
            Debug.Log("Ammo used. Remaining ammo: " + currentAmmo);

            GameController.instance.UpdateRemainingAmmoUI();
        }
    }
    
    public void AddAmmo(int amount)
    {
        int actualAmount = amount + ammoReward; 
        Debug.Log("ammoReward: " + ammoReward + ", maxAmmoCapacity: " + maxAmmoCapacity);
        int newAmmo = Mathf.Min(currentAmmo + actualAmount, maxAmmoCapacity);
        
        // log only if ammo actually changes
        if (newAmmo != currentAmmo) {
            currentAmmo = newAmmo;
            Debug.Log("Ammo added. Current ammo: " + currentAmmo + "/" + maxAmmoCapacity);

            GameController.instance.UpdateRemainingAmmoUI();
        }
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
    
    // THIS IS FOR UPGRADE#1 TO UPGRADE MAX AMMO CAP. CALLED ONCE PER PURCHASE
    public void UpdateMaxAmmoCapacity()
    {
        Debug.Log("curr max amo cap: " + maxAmmoCapacity);
        maxAmmoCapacity += 2;

        GameController.instance.UpdateRemainingAmmoUI();
    }

    // THIS IS FOR UPGRADE#2 TO UPGRADE MAX AMMO REWARD. CALLED ONCE PER PURCHASE
    public void UpdateMaxAmmoReward()
    {
        Debug.Log("curr max ammo reward: " + ammoReward + "UPDATING MAX AMMO REWARD NOW.");
        ammoReward += 1;
    }

    public int GetMaxAmmoReward()
    {
        return ammoReward;
    }
    
    public void SetUpgrades(int newAmmoCap, int newAmmoReward)
    {
        maxAmmoCapacity = newAmmoCap;
        ammoReward = newAmmoReward;
        currentAmmo = maxAmmoCapacity;
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

    private void OnBecameInvisible()
    {
        if (!Application.isPlaying) 
            return;

        if (!ScoreAndMoneyManager.instance.isAlive) 
            return;

        Die();
    }

    // death 
    public void Die()
    {
        if (!ScoreAndMoneyManager.instance.isAlive) return; 
        // update high score and total currency BEFORE resetting and bEFORE displaying end screen so its updated properly!!
        GameController.instance.UpdateHighScoreAndTotalCurrency(); 
        GameController.instance.UpdateUpgrades();
        
        ScoreAndMoneyManager.instance.SetPlayerDead(); // set player as dead; just a setter method to set isAlive variable to false
        
        // show end screen
        if (endScreen != null)
        {
            
            endScreen.Show(ScoreAndMoneyManager.instance.score);
            pauser.PauseGame();
        }
        else
        {
            Debug.LogError("EndScreen instance not found!");
        }

        //Disable player controls
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.enabled = false;
    }
}