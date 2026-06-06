using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform player;

    private bool hasSpawned = false;

    void Update()
    {
        if (hasSpawned) return;

        if (player.position.y < transform.position.y - 3f)
        {
            hasSpawned = true;

            float treeHeight = GetComponent<SpriteRenderer>().bounds.size.y;
            Vector3 spawnPos = transform.position + Vector3.down * treeHeight;

            Instantiate(treePrefab, spawnPos, Quaternion.identity);
        }
    }
}