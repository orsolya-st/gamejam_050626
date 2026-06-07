using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
        }
    }
}
