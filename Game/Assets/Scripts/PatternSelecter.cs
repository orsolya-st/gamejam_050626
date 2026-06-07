using Unity.Cinemachine;
using UnityEngine;

public class PatternSelecter : MonoBehaviour

{
    public int lastTreeNumber = 8;
    public SpriteRenderer spriteRenderer;
    public Sprite endTreeSprite;
    private BoxCollider2D confine;
    private CinemachineConfiner2D confiner2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        confine = GameObject.Find("CameraConfine").GetComponent<BoxCollider2D>();
        confiner2D = GameObject.Find("CinemachineCamera").GetComponent<CinemachineConfiner2D>();
    
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        if (TreeSpawner.treeCount == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        else if (TreeSpawner.treeCount == lastTreeNumber) // end tree case
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            spriteRenderer.sprite = endTreeSprite;
            // spriteRenderer.color = new Color32(255, 228, 225,255);
            // transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
            transform.localPosition += new Vector3(-0.2f,-0.5f,0f);
            confine.size += new Vector2(0f, 38f);
            confiner2D.InvalidateBoundingShapeCache();
        }
        else
        {
            int randomIndex = Random.Range(1, transform.childCount - 1);
            transform.GetChild(randomIndex).gameObject.SetActive(true);
            confine.size += new Vector2(0f,43f);
            confiner2D.InvalidateBoundingShapeCache();
        }
    }
}