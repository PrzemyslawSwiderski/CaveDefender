using UnityEngine;
using System.Collections;
using System;
using KiiCorp.Cloud.Analytics;
using System.Collections.Generic;
using KiiCorp.Cloud.Storage;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{	
	public float fullHealth = 100f;					// The player's health.
	public float health;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player
	public Transform respawnPoint;
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private EasyMovement playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player
	public Boolean alive = true;
	public float timer;
	public float timerToResurrection;

	void Awake ()
	{
		// Setting up references.
		playerControl = GetComponent<EasyMovement> ();
		//healthBar = GameObject.Find ("HealthBar").GetComponent<SpriteRenderer> ();
		healthBar = transform.Find ("HealthBar").GetComponent<SpriteRenderer> ();
		anim = gameObject.GetComponent<Animator> ();
		health = fullHealth;
		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
	}

	void Start ()
	{
		timer = 0.0f;
		timerToResurrection = 2.0f;
	}

	void Update ()
	{

	}

	void OnCollisionStay2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if ((col.gameObject.tag == "Enemy" && !col.gameObject.GetComponent<Enemy> ().dead) || 
			(col.gameObject.tag == "Enemy2" && !col.gameObject.GetComponent<Enemy2> ().dead) || 
			(col.gameObject.tag == "Boss" && !col.gameObject.GetComponent<EnemyBoss> ().dead)) {

			if (Time.time > lastHitTime + repeatDamagePeriod) {
				if (health > 0f) {
					TakeDamage (col.transform); 
					lastHitTime = Time.time; 
				} else {
					Death ();
				}
			}
		}
		
		if ((col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")) {
			anim.speed=0;
			if (!alive)
				Resurrection ();
		}
	}
	void OnCollisionExit2D (Collision2D col)
	{
		anim.speed = 1;
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Loot" && col.IsTouching (GetComponent<BoxCollider2D> ())) {
			GameManager.instance.bones += 1;
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "buildStone" && col.IsTouching (GetComponent<BoxCollider2D> ())) {
			GameManager.instance.buildStones += 1;
			Destroy (col.gameObject);
		}

		if (col.tag == "Bullet" || col.tag == "Spear") {
			if (health > 0f) {
				TakeDamage (col.transform); 
			} else {
				Death ();
			}
			if (col.gameObject.tag == "Bullet") {
				Destroy (col.gameObject);
			}
		}
	}

	void Resurrection ()
	{
		anim.speed=1;
		if (health < fullHealth) {
			transform.Find ("RightArm").Find ("Bow").gameObject.SetActive (false);
			transform.Find ("RightArm").Find ("Spear").gameObject.SetActive (false);
			health += 0.25f;
			UpdateHealthBar ();
		} else {
			Respawn ();
		}
	}

	void Respawn ()
	{
		health = fullHealth;
		GetComponent<EasyMovement> ().enabled = true;
		GetComponent<Rigidbody2D> ().isKinematic = false;
		GetComponentInChildren<ChangeWeapon> ().enabled = true;

		transform.Find ("RightArm").Find ("Bow").gameObject.SetActive (true);
		transform.Find ("RightArm").Find ("Spear").gameObject.SetActive (true);

		alive = true;

		anim.SetTrigger ("Respawn");
		transform.position = respawnPoint.position;
		UpdateHealthBar ();
	}

	public void TakeDamage (Transform enemy)
	{
		// Reduce the player's health by 10.
		health -= damageAmount;

		// Update what the health bar looks like.
		UpdateHealthBar ();

	}

	public void TakeDamageFromArrow (int damage)
	{
		// Reduce the player's health by 10.
		health -= damage;
		
		// Update what the health bar looks like.
		UpdateHealthBar ();
	}

	void Death ()
	{	
		alive = false;
		GameManager.instance.displayNotify (gameObject.tag + " is dead, touch him to ressurect.");
		GetComponent<EasyMovement> ().enabled = false;
		GetComponent<Rigidbody2D> ().isKinematic = true;
		GetComponentInChildren<ChangeWeapon> ().enabled = false;
		transform.Find ("RightArm").Find ("Bow").gameObject.SetActive (false);
		transform.Find ("RightArm").Find ("Spear").gameObject.SetActive (false);
		anim.SetTrigger ("Die");
	}

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp (Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3 (healthScale.x * health * 0.01f, 1, 1);
	}
}
