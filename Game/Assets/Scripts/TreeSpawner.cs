using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform player;

    private bool hasSpawned = false;

    public static int treeCount;

    private int maxTrees;


    private void Start()
    {
        maxTrees = GameObject.Find("Tree").GetComponent<PatternSelecter>().lastTreeNumber;
    }


    void Update()
    {
        if (hasSpawned || treeCount >= maxTrees) return;

        Debug.Log("Player Y: " + player.position.y + " | Tree Y + 2: " + (transform.position.y + 2f));

        if (player.position.y < transform.position.y - 2f)
        {
            Debug.Log("AUTO SPAWN TRIGGERED");

            hasSpawned = true;

            float treeHeight = GetComponent<SpriteRenderer>().bounds.size.y;
            Vector3 spawnPos =
                transform.position +
                Vector3.down * treeHeight +
                new Vector3(0, 0.2f, 0);

            treeCount++;

            GameObject newTree = Instantiate(treePrefab, spawnPos, Quaternion.identity);

            TreeSpawner spawner = newTree.GetComponent<TreeSpawner>();
            spawner.player = player;
            spawner.treePrefab = treePrefab;
        }
    }
}





