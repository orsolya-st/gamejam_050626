using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageHandler : MonoBehaviour
{
	private Rigidbody2D rb;
	public float threshold = 2.5f;
	public float multiplier = 0.5f;

	public float maxHealth = 1f;
	public float health;
	public float minSize = 0.4f;

	private float lastYVelocity;
	private bool falling;
	private float fallTime;
	public float maxFallingTime = 2f;


	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		threshold = 10f;
		multiplier = 0.1f;
	
		maxHealth = 1f;
		minSize = 0.8f;
		health = maxHealth;
	}

	private void FixedUpdate()
	{
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

	private void OnCollisionEnter2D(Collision2D other)
	{
		// Debug.LogWarning("collide");
		//on collision check fall damage
		float fallSpeed = lastYVelocity;		
		if (fallSpeed < -threshold)
		{
			float fallDamage = (-fallSpeed - threshold) * multiplier;
			UpdateHealth(health - fallDamage);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Minidrop"))
		{
			UpdateHealth(health + 0.2f);
			Destroy(other.gameObject);
			return;	
		}
	}


	public void UpdateHealth(float health)
	{	
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
		if(reason == "hole")
		{
			SceneManager.LoadScene(3, LoadSceneMode.Single);
			Debug.Log("Died from hole");
		} else if (reason == "fallDamage")
		{
			Debug.Log("Died from fall damage");
		} else if (reason == "fallOut")
		{
			Debug.Log("Fell too much");
		}
		
        Destroy(gameObject);
	}
}
