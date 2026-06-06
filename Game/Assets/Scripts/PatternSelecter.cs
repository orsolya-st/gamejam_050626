using UnityEngine;

public class PatternSelecter : MonoBehaviour
{


    bool isStarting = true;
    void Start()
    {
        
        if (isStarting)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);

            int randomIndex = Random.Range(1, transform.childCount);

            transform.GetChild(randomIndex).gameObject.SetActive(true);
        }

    }
}