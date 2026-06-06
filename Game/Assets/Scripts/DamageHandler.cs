using System;
using UnityEngine;
using Object = System.Object;

public class DamageHandler : MonoBehaviour
{
	private Rigidbody2D rb;
	public float threshold = 2.5f;
	public float multiplier = 0.5f;

	public float maxHealth = 1f;
	public float health;
	public float minSize = 0.4f;

	private float lastYVelocity;


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
		lastYVelocity = rb.linearVelocityY;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		float fallSpeed = lastYVelocity;		
		if (fallSpeed < -threshold)
		{
			float fallDamage = (-fallSpeed - threshold) * multiplier;
			Debug.Log($"Fell with velocity:{fallSpeed}. Fall damage{fallDamage}");
			UpdateHealth(health - fallDamage);
			// Debug.Log($"New health:{health - fallDamage}");
		}
		else
		{
			Debug.Log($"Fell with velocity:{fallSpeed}. No fall damage");
		}
	}
	

	public void UpdateHealth(float health)
	{
		this.health = health;
		Transform transform = GetComponent<Transform>();
		float healthScaling = minSize + (1 - minSize) * (this.health / maxHealth);
		transform.localScale = new Vector3(healthScaling,healthScaling,healthScaling);
		Debug.Log("UpdatedScale and health");
		
		if(this.health <= 0f)
		{
			Debug.Log("Die");
			Die();
		}
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
