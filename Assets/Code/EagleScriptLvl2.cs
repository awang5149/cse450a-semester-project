using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EagleScriptLvl2 : MonoBehaviour
{
    // Movement parameters
    public float minAmplitude = 1f;
    public float maxAmplitude = 3f;
    public float minVerticalSpeed = 0.5f;
    public float maxVerticalSpeed = 2f;
    public float minHorizontalSpeed = 2f;
    public float maxHorizontalSpeed = 5f;

    public float topBoundary = 5f; 
    public float groundBoundary = -3f;
    private float amplitude;
    private float verticalSpeed;
    private float horizontalSpeed;

    private Vector3 startPosition;
    private float phaseOffset;
    
    public EndScreen endScreen;
    public int score = 0;

    void Start()
    {
        startPosition = transform.position;

        // Random Movements
        amplitude = Random.Range(minAmplitude, maxAmplitude);
        verticalSpeed = Random.Range(minVerticalSpeed, maxVerticalSpeed);
        horizontalSpeed = Random.Range(minHorizontalSpeed, maxHorizontalSpeed);
        phaseOffset = Random.Range(0f, Mathf.PI * 2); 
    }

    void Update()
    {
        float sinWave = Mathf.Sin(Time.time * verticalSpeed + phaseOffset) * amplitude;
        float newY = startPosition.y + sinWave;

        if (newY >= topBoundary)
        {
            newY = topBoundary;
            amplitude = -Mathf.Abs(amplitude);
        }
        else if (newY <= groundBoundary)
        {
            newY = groundBoundary;
            amplitude = Mathf.Abs(amplitude);
        }

        float newX = transform.position.x - horizontalSpeed * Time.deltaTime;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetScene();
        }
        else if (other.CompareTag("Boundary") || other.CompareTag("Ground"))
        {
            amplitude = -amplitude; 
        }
    }

    void ResetScene()
    {
            //Currently displays Game Over on collision; edit to display after losing all lives
            if (endScreen != null)
            {
                endScreen.Show(score);
                Time.timeScale = 0f; // Pause the game
            }

            // Reload Scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
