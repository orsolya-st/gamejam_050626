using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // By making these public, they will show up in the Unity Inspector
    public TMP_Text timeText; 
    public float timeRemaining = 89f; // Give it a starting time!
    
    private bool timerIsRunning = false;

    public GameObject targetObject;
    public DamageHandler damageHandler;

    private void Start()
    {
        damageHandler = targetObject.GetComponent<DamageHandler>();
    }

    void Update()
    {
        // 1. Wait for input to start
        // If the timer isn't running yet AND we still have time left...
        if (!timerIsRunning && timeRemaining > 0)
        {
            // Update the display so the player sees the starting time
            DisplayTime(timeRemaining); 

            if (Input.anyKey)
            {
                timerIsRunning = true; // Start the clock!
            }
            else
            {
                return; // Stop the code here until they press a key
            }
        }
        
        // 2. The countdown logic
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                // Time has run out! Stop the timer so it doesn't go into negative numbers.
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);
                Debug.Log("Time's up!");
            }
        }

        if (timeRemaining <= 0)
        {
            //SceneManager.LoadScene(3, LoadSceneMode.Single);
            damageHandler.Die("Timer");
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}