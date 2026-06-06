using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScene : MonoBehaviour
{
    public AudioSource mySpeaker;
    public AudioClip jingle;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            // Start the "coroutine" instead of switching scenes immediately
            StartCoroutine(PlaySoundAndLoad());
        }
    }

    IEnumerator PlaySoundAndLoad()
    {
        // 1. Play the sound
        mySpeaker.PlayOneShot(jingle);

        // 2. Wait for the length of the sound clip (or a fixed time)
        yield return new WaitForSeconds(jingle.length - 0.5f); 

        // 3. Now it is safe to load the scene
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
