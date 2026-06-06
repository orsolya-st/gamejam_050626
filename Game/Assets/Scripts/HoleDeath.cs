using UnityEngine;

public class HoleDeath : MonoBehaviour
{
    void Awake()
    {
        // This tells Unity NOT to destroy this object when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}
