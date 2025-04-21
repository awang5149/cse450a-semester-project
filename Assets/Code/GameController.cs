using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public HamtoroController hamtoroController;
    public PowerupManager powerupManager;

    public GameObject seedPrefab;
    public TMP_Text textScore;
    public TMP_Text textMoney;
    public int highScore = 0; // all time high score, initialize to 0.
    public int totalCurrency; // player currency

    public int ammoCap = 5; // Default value
    public int ammoReward = 1; // Default value
    public int powerupDuration = 5; // Default value
    
    public int[] shopPrices = new int[3];     // Index 0 = cap, 1 = reward, 2 = duration

    public TMP_Text remainingAmmoUI_TXT; // update remaining ammo in top left corner

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // Register the scene loaded event right away in Awake
            SceneManager.sceneLoaded += OnSceneLoaded;
            if (ammoCap <= 0) ammoCap = 5;
            if (ammoReward <= 0) ammoReward = 1;
            if (powerupDuration <= 0) powerupDuration = 5;
            
            if (shopPrices[0] == 0) shopPrices[0] = 25;
            if (shopPrices[1] == 0) shopPrices[1] = 50;
            if (shopPrices[2] == 0) shopPrices[2] = 35;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // If we're already in a gameplay scene when this is instantiated, find references immediately
        FindReferences();
        UpdateDisplay();
    }

    void OnDestroy()
    {
        // Always unregister event when object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Wait a frame to ensure all objects are initialized
        StartCoroutine(SetupAfterSceneLoad());
    }

    IEnumerator SetupAfterSceneLoad()
    {
        yield return new WaitForEndOfFrame();
        
        FindReferences();
        
        // Apply saved upgrades to the player and powerup manager
        ApplyUpgrades();
        
        UpdateDisplay();
    }

    void FindReferences()
    {
        textScore = GameObject.Find("Text - Score")?.GetComponent<TMP_Text>();
        textMoney = GameObject.Find("Text - Money")?.GetComponent<TMP_Text>();
        remainingAmmoUI_TXT = GameObject.Find("remaining ammo count TXT")?.GetComponent<TMP_Text>();
        
        hamtoroController = FindObjectOfType<HamtoroController>();
        powerupManager = FindObjectOfType<PowerupManager>();
        
    }

    void ApplyUpgrades()
    {
        // Apply upgrades to hamtoro controller
        if (hamtoroController != null)
        {
            hamtoroController.SetUpgrades(ammoCap, ammoReward);
            UpdateRemainingAmmoUI();
        }

        // Apply upgrades to powerup manager
        if (powerupManager != null)
        {
            powerupManager.SetPowerupLength(powerupDuration);
        }
    }

    public void UpdateDisplay()
    {
        if (ScoreAndMoneyManager.instance != null)
        {
            if (textScore != null)
                textScore.text = "Score: " + ScoreAndMoneyManager.instance.score.ToString();
            
            if (textMoney != null)
                textMoney.text = "Currency: " + ScoreAndMoneyManager.instance.money.ToString();
        }
    }

    // NEED TO CALL BEFORE RESETTING SCORE AND MONEY!!
    // for currency, need to update after each run
    public void UpdateHighScoreAndTotalCurrency(){
        if (ScoreAndMoneyManager.instance.score > highScore){
            highScore = ScoreAndMoneyManager.instance.score;
        }
        totalCurrency += ScoreAndMoneyManager.instance.money;
    }

    public void UpdateUpgrades()
    {
        if (hamtoroController != null)
        {
            ammoCap = hamtoroController.GetMaxAmmoCapacity();
            ammoReward = hamtoroController.GetMaxAmmoReward();
        }
        
        if (powerupManager != null)
        {
            powerupDuration = powerupManager.GetPowerupLength();
        }
    }

    // call everytime ammo is used
    public void UpdateRemainingAmmoUI(){
        if (remainingAmmoUI_TXT != null && hamtoroController != null)
        {
            Debug.Log("Updating remaining Ammo UI. Current ammo: " + hamtoroController.GetCurrentAmmo().ToString());
            remainingAmmoUI_TXT.text = "x " + hamtoroController.GetCurrentAmmo().ToString();
        }
        else
        {
            Debug.LogWarning("Can't update ammo UI - Missing references");
        }
    }
}