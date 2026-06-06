using UnityEngine;

public class TreeDecorations : MonoBehaviour
{
    public GameObject[] decorationPrefabs;

    public int minAmount = 2;
    public int maxAmount = 6;

    public float minY = -4f;
    public float maxY = 4f;

    public float minScale = 0.5f;
    public float maxScale = 1.5f;

    void Start()
    {
        int amount = Random.Range(minAmount, maxAmount + 1);

        for (int i = 0; i < amount; i++)
        {
            GameObject prefab = decorationPrefabs[
                Random.Range(0, decorationPrefabs.Length)
            ];

            float randomY = Random.Range(minY, maxY);

            float side = Random.value < 0.5f ? -1f : 1f;
            float randomX = side * Random.Range(1.5f, 3f);

            Vector3 spawnPos = transform.position + new Vector3(randomX, randomY, 0);

            GameObject decoration = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

            float randomScale = Random.Range(minScale, maxScale);
            decoration.transform.localScale = new Vector3(
                randomScale * side,
                randomScale,
                1
            );
        }
    }
}