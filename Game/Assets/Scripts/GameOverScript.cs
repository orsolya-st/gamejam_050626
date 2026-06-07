using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Timers;

public class GameOverScript : MonoBehaviour
{
    public GameObject image;
    private float elapsed;

    [SerializeField]
    private float duration = 2.0f;

    void Start()
    {
        image.SetActive(false);
        elapsed = 0.0f;
    }


    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= duration) { 
           image.SetActive(true);
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }

        }
    }
}