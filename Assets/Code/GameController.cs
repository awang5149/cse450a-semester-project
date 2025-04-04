using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject seedPrefab;
    public TMP_Text textScore;
    public TMP_Text textMoney;
    // public GameObject gameOverScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
            // Debug.Log($"updating display - score: {ScoreAndMoneyManager.instance.score}, money: {ScoreAndMoneyManager.instance.money}");
        }
    }

    /*
    public void ShowGameOver(){
        gameOverScreen.SetActive(true);
    }
    */
}