using System;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject treePrefab;
    [SerializeField]
    private GameObject treeEndPrefab;
    public Transform player;
    [SerializeField]
    private static int maxTreeCount = 1;

    private bool hasSpawned = false;

    public static int treeCount;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (hasSpawned == false && treeCount < maxTreeCount)
        {
            if (player.position.y < transform.position.y - 3f)
            {
                hasSpawned = true;

                float treeHeight = spriteRenderer.bounds.size.y;
                Vector3 spawnPos = transform.position + Vector3.down * treeHeight + new Vector3(0, 0.2f, 0);
                
                treeCount++;
                Debug.Log($"Spawned Treee{treeCount}");
                if(treeCount == maxTreeCount)
                {
                    spawnPos += new Vector3(0.27f,0.54f,0);
                    Instantiate(treeEndPrefab, spawnPos, Quaternion.identity);
                }
                else Instantiate(treePrefab, spawnPos, Quaternion.identity);
            }
        }

        // Delete trees that are far above the player
        if (player.position.y < transform.position.y - 40f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}