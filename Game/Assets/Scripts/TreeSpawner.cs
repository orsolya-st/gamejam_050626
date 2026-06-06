using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform spawnPoint;

    private bool hasSpawned = false;
    private static float nextAllowedSpawnTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (hasSpawned) return;

        if (Time.time < nextAllowedSpawnTime) return;

        hasSpawned = true;
        nextAllowedSpawnTime = Time.time + 1f;

        Instantiate(treePrefab, spawnPoint.position, Quaternion.identity);

        gameObject.SetActive(false);
    }
}