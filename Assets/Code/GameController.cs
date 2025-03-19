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

    public int score;
    public int money;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
        
    }
    void UpdateDisplay()
    {
        textScore.text = score.ToString();
        textMoney.text = money.ToString();
    }

    public void EarnPoints(int pointAmount)
    {
        score += pointAmount;
        money += pointAmount;
    }

}
