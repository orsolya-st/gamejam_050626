using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScene : MonoBehaviour
{
    public AudioSource mySpeaker;
    public AudioClip jingle;

    private float waitTimer = 10f; // 10-second countdown
    private bool isReady = false;
    private bool isLoading = false;

    void Update()
    {
        // 1. Handle the countdown timer
        if (!isReady)
        {
            // Time.deltaTime is the time in seconds it took to complete the last frame
            waitTimer -= Time.deltaTime; 

            if (waitTimer <= 0f)
            {
                isReady = true;
            }
        }

        // 2. Handle the player input once the timer hits 0
        if (isReady && !isLoading && Input.anyKeyDown)
        {
            isLoading = true;
            StartCoroutine(PlaySoundAndLoad());
        }
    }

    IEnumerator PlaySoundAndLoad()
    {
        if (mySpeaker != null && jingle != null)
        {
            mySpeaker.PlayOneShot(jingle);
        }

        // Keep this short delay so the jingle has time to play before the scene cuts
        yield return new WaitForSeconds(0.4f); 

        SceneManager.LoadScene(0, LoadSceneMode.Single); 
    }
}