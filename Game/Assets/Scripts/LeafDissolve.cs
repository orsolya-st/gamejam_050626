using System;
using System.ComponentModel;
using UnityEngine;

public class LeafDissolve : MonoBehaviour
{
    private float standingTime;
    [SerializeField]
    private float maxStandingTime = 2f;

    private bool isStoodOn = false;
    private bool isDissolving = false;
    private Material _material;
    private float fade;
    private float disolveTime;

    private BoxCollider2D collider;

    //audio stuff
    public AudioSource speaker;
    public AudioClip leafesound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        fade = 1;
        standingTime = 0;
        isStoodOn = false;
        isDissolving = false;
        _material = GetComponent<SpriteRenderer>().material;
        disolveTime = maxStandingTime * 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            if (fade <= 0.5)
            {
                Destroy(collider);
            }
            if (fade <= 0)
            {
                Destroy(gameObject);
            }

            fade -= Time.deltaTime / disolveTime;
            _material.SetFloat("_Fade",fade);
        }
        
        if (isStoodOn)
        {
            standingTime += Time.deltaTime;
        }
        else
        {
            standingTime = 0;
        }

        if (!isDissolving && standingTime >= maxStandingTime / 2)
        {
            isDissolving = true;

            //audio
            speaker.PlayOneShot(leafesound);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isStoodOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isStoodOn = false;
            standingTime = 0f;
        }
    }
    
}
