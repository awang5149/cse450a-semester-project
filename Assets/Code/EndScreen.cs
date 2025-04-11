using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TMP_Text finalSeedsCollectedText;
    public TMP_Text finalScoreText;
    public TMP_Text finalHighScoreText;
    public TMP_Text cumulativeSeedsCollectedText;
    
    public void Show(int score)
    {
        gameObject.SetActive(true); // set canvas to active so it shows
        // Update the seeds text
        if (finalSeedsCollectedText != null && ScoreAndMoneyManager.instance != null)
        {
            finalSeedsCollectedText.text = "Seeds Collected: " + ScoreAndMoneyManager.instance.money.ToString();
            cumulativeSeedsCollectedText.text = "Total Currency: " + GameController.instance.totalCurrency.ToString();
        }
        // update last run score and high score
        if (ScoreAndMoneyManager.instance != null)
        {
            finalScoreText.text = "Score: " + ScoreAndMoneyManager.instance.score.ToString(); 
            finalHighScoreText.text = "High Score: " + GameController.instance.highScore.ToString(); 
        }
    }

    public void Hide(){
        gameObject.SetActive(false); // hide canvas again
    }
    
    public void RestartGame()
    {
        ScoreAndMoneyManager.instance.ResetScoreAndMoney(); // reset score and seeds right before restarting game so that u can still display it on game over screen
        Time.timeScale = 1f;
        GameController.instance.UpdateDisplay(); // update display so it shows reset score and monye
        Hide(); // hide end screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}