using System.Collections;
using UnityEngine;

public class ScoreAndMoneyManager : MonoBehaviour
{
    public static ScoreAndMoneyManager instance;
    // public static ShopManagerScript shopManagerScript;
    
    public int score = 0;
    public int money = 0;
    public bool isAlive { get; private set; } = true;
    public int seedMultiplier = 1; // added this for 2x seedMultiplier power up (TO BE IMPLEMENTED)
    
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

    void Start(){ 
        StartCoroutine(IncreaseScoreOverTime()); // accumulating score
        StartCoroutine(UpdateMoneyOverTime()); // Sync money with seed count
    }
    
    // change state of alive/dead for player
    public void SetPlayerDead()
    {
        isAlive = false;
    }

    IEnumerator IncreaseScoreOverTime(){
        while (isAlive) {
            // Debug.Log($"score increased to {score}");
            yield return new WaitForSeconds(.1f); // increase score every second
            score++; 
            GameController.instance?.UpdateDisplay(); // update UI when score changes
        }
    }

    IEnumerator UpdateMoneyOverTime()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(0.5f); // Check seed count every 0.5 sec
            GameController.instance?.UpdateDisplay();
        }
    }
    
    public void AddScore(int amount) {
        score += amount;
        GameController.instance?.UpdateDisplay(); // update UI when score changes
    }

    public void AddMoney(int moneyAmount) {
        money += moneyAmount;
        GameController.instance?.UpdateDisplay(); // update UI when $ changes
        // shopManagerScript.UpdateMoney(); // when updating $ collected during run, also update total $
    }
    
    // DOUBLE CHECK THAT THIS METHOD ANME WAS RIGHT
    public void ResetScoreAndMoney() {
        score = 0;
        // isAlive = false; // stop increasing score after death
        money = 0;
    }
    
}