using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseAndResume : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.instance.PlaySoundPauseResume();
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
        if (gameIsPaused && Input.GetKeyDown(KeyCode.Space))
        {
            Input.ResetInputAxes();
        }
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
    }
}