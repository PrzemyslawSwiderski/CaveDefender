using UnityEngine;
using System.Collections;
using System;
using KiiCorp.Cloud.Analytics;
using System.Collections.Generic;
using KiiCorp.Cloud.Storage;

public class CaveHealth : MonoBehaviour
{
	public float fullHealth = 100f;                 // The cave's health.
	public float health;                    // The cave's health.
	public float repeatDamagePeriod = 2f;       // How frequently the cave can be damaged.
	//public AudioClip[] ouchClips;               // Array of clips to play when the cave is damaged.
	public float damageAmount = 5f;            // The amount of damage to take when enemies touch the cave
	private SpriteRenderer healthBar;           // Reference to the sprite renderer of the health bar.
	private Vector3 healthScale;                // The local scale of the health bar initially (with full health).
	public ParticleSystem WhiteSmoke;           //First smoke prefab goes here.
	public ParticleSystem DenseSmoke;           //Second smoke prefab goes here.

	void Start ()
	{
	}

	void Awake ()
	{
		//healthBar = GameObject.Find ("HealthBar").GetComponent<SpriteRenderer> ();
		healthBar = transform.Find ("HealthBar").GetComponent<SpriteRenderer> ();
		//health = fullHealth;
		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
	}

	void Update ()
	{
		Smokey ();
		Dense ();
		if (health >= 50f) {
			if (WhiteSmoke.isPlaying) {
				WhiteSmoke.Stop ();
			}
			if (DenseSmoke.isPlaying) {
				DenseSmoke.Stop ();
			}
		}
		if (health == 0.0f)
			GameManager.instance.actualGameState = gameState.GameOver;

	}
		
	void Dense ()
	{
		if (health < 25f) {
			if (WhiteSmoke.isPlaying) {
				WhiteSmoke.Stop ();
			}
			if (!DenseSmoke.isPlaying) {
				DenseSmoke.Play ();
			}
		}
	}

	void Smokey ()
	{
		if (health < 50f) {
			if (!WhiteSmoke.isPlaying) {
				WhiteSmoke.Play ();
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy2" || col.gameObject.tag == "Bullet") {
			if (health > 0.0f) 
				TakeDamage (col.transform);
			GameManager.instance.deadEnemies++;
			Destroy (col.gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Bullet" || col.tag == "Spear") 
		if (health > 0.0f) 
			TakeDamage (col.transform);
	}

	public void TakeDamage (Transform enemy)
	{
		// Reduce the cave's health by 5.
		health -= damageAmount;

		UpdateHealthBar ();
	}

	public void AddHp (int Amount)
	{
		health += Amount;
		
		UpdateHealthBar ();
	}

	public void getHealth (Transform enemy)
	{
		// Reduce the cave's health by 5.
		health += damageAmount;

		// Update what the health bar looks like.
		UpdateHealthBar ();
	}

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp (Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3 (healthScale.x * health * 0.01f, 1, 1);
	}

}
