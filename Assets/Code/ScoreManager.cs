using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    private int score = 0;
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

    void Start()
    {
        UpdateScoreUI();
        StartCoroutine(IncreaseScoreOverTime()); // Start accumulating score
    }

    IEnumerator IncreaseScoreOverTime()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(1f); // Increase score every second
            score++;
            UpdateScoreUI();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        score = 0;
        isAlive = false; // Stop increasing score after death
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}