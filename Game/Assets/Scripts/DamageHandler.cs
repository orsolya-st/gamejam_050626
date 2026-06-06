using System;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
	public float threshold = 0.05f;
	public float multiplier = 5f;

	public float health = 1;

	private void OnCollisionEnter2D(Collision2D other)
	{
		float fallSpeed = GetComponent<Rigidbody2D>().linearVelocityY;	
		if (fallSpeed > threshold)
		{
			float fallDamage = (fallSpeed - threshold) * multiplier;
			Debug.Log($"Fell with velocity:{fallSpeed}. Fall damage{fallDamage}");
			health -= fallDamage;
		}
		else
		{
			Debug.Log($"Fell with velocity:{fallSpeed}. No fall damage");
		}
	}
}
