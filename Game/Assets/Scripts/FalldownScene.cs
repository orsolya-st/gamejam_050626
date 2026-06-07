using UnityEngine;

public class FalldownScene : MonoBehaviour
{
    public GameObject targetObject;

    void Awake()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        Destroy(targetObject);
        // This tells Unity NOT to destroy this object when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}
