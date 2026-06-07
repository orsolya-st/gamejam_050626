using UnityEngine;
using System.Collections;

public class StartSceneIntro : MonoBehaviour
{
    public GameObject background;
    public GameObject startButton;

    public AudioSource introAudio;
    public AudioSource menuAudio;

    public Animator introAnimation;

    void Start()
    {
        background.SetActive(false);
        startButton.SetActive(false);

        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // play 10 sec animation
        if (introAnimation != null)
            introAnimation.Play("YourAnimationName");

        // play 10 sec audio
        if (introAudio != null)
            introAudio.Play();

        // wait 10 seconds
        yield return new WaitForSeconds(10f);

        // show menu
        background.SetActive(true);
        startButton.SetActive(true);

        // optional: play menu music/jingle
        if (menuAudio != null)
            menuAudio.Play();
    }
}