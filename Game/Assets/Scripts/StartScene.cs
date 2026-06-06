using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public AudioSource mySpeaker;
    public AudioClip jingle;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            mySpeaker.PlayOneShot(jingle);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
