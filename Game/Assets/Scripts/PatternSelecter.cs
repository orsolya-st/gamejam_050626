using UnityEngine;

public class PatternSelecter : MonoBehaviour

{
    [SerializeField]
    private int lastTreeNumber = 8;

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

        else if (TreeSpawner.treeCount == lastTreeNumber)
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        }
        else
        {
            int randomIndex = Random.Range(1, transform.childCount - 1);
            transform.GetChild(randomIndex).gameObject.SetActive(true);
        }
    }
}