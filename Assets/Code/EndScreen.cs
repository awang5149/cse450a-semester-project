using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text seedsCollected;
    
    public void Show(int score)
    {
        // activate this GameObject itself
        gameObject.SetActive(true);
        
        // Update the seeds text
        if (seedsCollected != null && ScoreAndMoneyManager.instance != null)
        {
            seedsCollected.text = "Seeds Collected: " + ScoreAndMoneyManager.instance.money.ToString();
        }
    }
    
    public void RestartGame()
    {
        ScoreAndMoneyManager.instance.ResetScore(); // reset score right before restarting game so that u can still display it on game over screen
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}