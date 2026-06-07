using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Timers;

public class GameOverScript : MonoBehaviour
{
    public GameObject image;
    private float elapsed;

    void Start()
    {
        image.SetActive(false);
        elapsed = 0.0f;
    }


    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= 2.0f) { 
           image.SetActive(true);
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("0_Main scene");
            }

        }
    }
}