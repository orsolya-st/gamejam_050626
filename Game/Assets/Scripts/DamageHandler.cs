using System;
using UnityEngine;
using Object = System.Object;
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
			Debug.Log("Started falling");
			fallTime = 0f;
		}
		else if(lastYVelocity < 0 && currentVelocity < 0)
		{
			if (fallTime >= maxFallingTime)
			{
				Die();
			}
			fallTime += Time.deltaTime;
		} else if(currentVelocity >= 0)
		{
			Debug.Log("Not falling");
			falling = false;
		}
		lastYVelocity = currentVelocity;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		float fallSpeed = lastYVelocity;		
		if (fallSpeed < -threshold)
		{
			float fallDamage = (-fallSpeed - threshold) * multiplier;
			UpdateHealth(health - fallDamage);
		}
	}
	

	public void UpdateHealth(float health)
	{
		this.health = health;
		Transform transform = GetComponent<Transform>();
		float healthScaling = minSize + (1 - minSize) * (this.health / maxHealth);
		//adjust size based on health
		transform.localScale = new Vector3(healthScaling,healthScaling,healthScaling);
		
		if(this.health <= 0f)
		{
			Die();
		}
	}

	public void Die()
	{
		SceneManager.LoadScene(3, LoadSceneMode.Single);
        Destroy(gameObject);
	}
}
