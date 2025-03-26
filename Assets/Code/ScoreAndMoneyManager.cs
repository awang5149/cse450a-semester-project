using System.Collections;
using UnityEngine;

public class ScoreAndMoneyManager : MonoBehaviour
{
    public static ScoreAndMoneyManager instance;
    
    public int score { get; private set; } = 0;
    public int money = 0;
    private bool isAlive = true; // Track if Hamtaro is alive
    
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
            money = SeedBehavior.GetSeedCount(); // Sync money with seed count
            GameController.instance?.UpdateDisplay();
        }
    }
    
    public void AddScore(int amount) {
        score += amount;
        GameController.instance?.UpdateDisplay(); // update UI when score changes
    }

    public void AddMoney(int moneyAmount) {
        money += moneyAmount;
        GameController.instance?.UpdateDisplay(); // update UI when money changes
    }
    
    public void ResetScore() {
        score = 0;
        isAlive = false; // stop increasing score after death
        GameController.instance?.UpdateDisplay(); // update UI when reset
    }
}
