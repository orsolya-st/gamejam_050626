using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform player;

    private bool hasSpawned = false;

    void Update()
    {
        if (hasSpawned == false)
        {
            if (player.position.y < transform.position.y - 3f)
            {
                hasSpawned = true;

                float treeHeight = GetComponent<SpriteRenderer>().bounds.size.y;
                Vector3 spawnPos = transform.position + Vector3.down * treeHeight + new Vector3(0, 0.2f, 0);

                Instantiate(treePrefab, spawnPos, Quaternion.identity);
            }
        }

        // Delete trees that are far above the player
        if (player.position.y < transform.position.y - 40f)
        {
            Destroy(gameObject);
        }
    }
}