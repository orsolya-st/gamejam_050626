using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int sceneIndex;

    private void OggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hole");
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}