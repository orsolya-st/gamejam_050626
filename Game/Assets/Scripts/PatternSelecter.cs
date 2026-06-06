using UnityEngine;

public class PatternSelecter : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        if (TreeSpawner.treeCount == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            int randomIndex = Random.Range(1, transform.childCount);
            transform.GetChild(randomIndex).gameObject.SetActive(true);
        }
    }
}