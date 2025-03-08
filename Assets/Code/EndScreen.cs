using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text seedsCollected;
    
    void Awake()
    {
        if (seedsCollected == null)
        {
            seedsCollected = GetComponentInChildren<Text>();
            
            if (seedsCollected == null)
            {
                Debug.LogError("Seeds Collected Text not found! Please assign it in the inspector.");
            }
        }
        
        gameObject.SetActive(false);
    }
    
    public void Show(int score)
    {
        gameObject.SetActive(true);
        seedsCollected.text = "Seeds Collected: " + score.ToString();
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);

    }
}