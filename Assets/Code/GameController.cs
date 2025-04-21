/*
GameController
- update high score and total cumulative money
- update display to show score + money collected on current run (show we move this to ScoreAndMoneyManager or some other class idk)

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public HamtoroController hamtoroController;

    public GameObject seedPrefab;
    public TMP_Text textScore;
    public TMP_Text textMoney;
    public int highScore = 0; // all time high score, initialize to 0.
    public int totalCurrency = 500; // player currency

    public TMP_Text remainingAmmoUI_TXT; // update remaining ammo in top left corner

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
    
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateDisplay(); // Ensure UI starts correctly
    }

    public void UpdateDisplay()
    {
        if (ScoreAndMoneyManager.instance != null)
        {
            textScore.text = "Score: " + ScoreAndMoneyManager.instance.score.ToString();
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

    // call everytime ammo is used
    public void UpdateRemainingAmmoUI(){
        Debug.Log("updating remaining Ammo UI now! Current ammo: " + hamtoroController.GetCurrentAmmo().ToString());
        remainingAmmoUI_TXT.text = "x " + hamtoroController.GetCurrentAmmo().ToString();
    }
}