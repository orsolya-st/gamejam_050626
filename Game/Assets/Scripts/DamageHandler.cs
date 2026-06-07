using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageHandler : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator animator;
	public float threshold = 12;
	public float multiplier = 0.1f;

	public float maxHealth = 1f;
	public float health;
	public float minSize = 0.4f;

	private float lastYVelocity;
	private bool falling;
	private float fallTime;
	public float maxFallingTime = 2f;
	public float fallingAnimTreshold = 0.2f;

	//audio stuff
	public AudioSource speaker;
    public AudioClip fallsound;

    public AudioClip splashSound;
	private bool isDead = false;


    private void Awake()
	{
		isDead = false;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		health = maxHealth;
	}

	private void FixedUpdate()
	{
		if (isDead) return;

		float currentVelocity = rb.linearVelocityY;
		if (lastYVelocity >= 0 && currentVelocity < 0)
		{
			falling = true;
			// Debug.Log("Started falling");
			fallTime = 0f;
		}
		else if(lastYVelocity < 0 && currentVelocity < 0)
		{
			if (fallTime >= maxFallingTime)
			{
				Debug.Log("fallout --> die call");
				Die("fallOut");
			}
			fallTime += Time.deltaTime;
		} else if(currentVelocity >= 0)
		{
			// Debug.Log("Not falling");
			falling = false;
		}
		lastYVelocity = currentVelocity;
	}

	private void Update()
	{
		if (falling && fallTime >= fallingAnimTreshold)
		{
			animator.SetBool("Falling",true);
		}
		else
		{
			animator.SetBool("Falling",false);
		}

	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (isDead) return;
		
		if(other.collider.CompareTag("Finish")){ //WIN THE GAME
			TreeSpawner.treeCount = 0;
			SceneManager.LoadScene(5, LoadSceneMode.Single);
		}
		float fallSpeed = lastYVelocity;        
    
    // Check if we hit hard enough
    if (fallSpeed < -threshold)
    {
        Debug.Log("Impact detected! Speed: " + fallSpeed);
        
        if (speaker != null && fallsound != null)
        {
            speaker.PlayOneShot(fallsound);
            Debug.Log("Sound played successfully.");
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing in the Inspector!");
        }

        float fallDamage = (-fallSpeed - threshold) * multiplier;
        UpdateHealth(health - fallDamage);
    }
}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (isDead) return;
		
		if (other.CompareTag("Minidrop"))
		{
			UpdateHealth(health + 0.2f);
            AudioSource.PlayClipAtPoint(splashSound, Camera.main.transform.position);
            Destroy(other.gameObject);
			return;	
		}
	}


	public void UpdateHealth(float health)
	{	
		if (isDead) return;
		
		this.health = health;
		if (health > maxHealth) this.health = maxHealth;

		Transform transform = GetComponent<Transform>();
		float healthScaling = minSize + (1 - minSize) * (this.health / maxHealth);
		//adjust size based on health
		transform.localScale = new Vector3(healthScaling,healthScaling,healthScaling);
		
		if(this.health <= 0f)
		{
			Die("fallDamage");
		}
	}

	public void Die(String reason)
	{
		Debug.Log("Die function triggered");
		
		if (isDead) return;
        isDead = true;
		
		TreeSpawner.treeCount = 0;
		if (reason == "hole")
		{
			TreeSpawner.treeCount = 0;
			SceneManager.LoadScene(2, LoadSceneMode.Single);
			Debug.Log("Died from hole");
			//Destroy(gameObject);
		} else if (reason == "fallDamage" || reason == "fallOut")
		{
			Debug.Log("Fell too much");
			TreeSpawner.treeCount = 0;
			SceneManager.LoadScene(3, LoadSceneMode.Single);
		} else if (reason == "Timer")
		{
			SceneManager.LoadScene(3,LoadSceneMode.Single);
		}
        //Destroy(gameObject);
    }
}
