using UnityEngine;

public class SplashScene : MonoBehaviour
{
    void Awake()
    {
        // This tells Unity NOT to destroy this object when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}
