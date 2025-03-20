using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class GameController : MonoBehaviour
{
    // removed textScore nad score variables bc theres alr score variable in ScoreManager script
    public static GameController instance;

    public GameObject seedPrefab;
    public TMP_Text textScore;
    public TMP_Text textMoney;

    // public int score;
    // public int money;

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
        }
        textMoney.text = "Currency: " + ScoreAndMoneyManager.instance.money.ToString();
    }
}