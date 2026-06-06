using UnityEngine;

public class MiniDroplet : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float size = Random.Range(0.3f, 0.7f);
        Vector2 targetSize = new Vector2(size, size);

        transform.localScale = targetSize;
    }
}
