using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour
{
	private float moveSpeed, moveSpeedY;		// The speed the enemy moves at.
	public int HP;                  // How many times the enemy can be hit before it dies.
	public int fullHP = 1000;					// The player's health.
	int bonesValue = 100;
	public bool dead = false;           // Whether or not the enemy is dead.
	private float xMin, xMax, yMin, yMax, movey, howManyY, howTimes;
	[HideInInspector]
	public bool
		facingRight = true;
	private Transform frontCheck;       // Reference to the position of the gameobject used for checking if something is in front.
	private Transform Player;
	private Transform Player2;
	private Transform Boss;
	private Animator anim;						// Reference to the Animator on the player
	private int helpVariable2 = 0;
	private float distancePlayer;
	private float distancePlayer2;
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).

	void Awake ()
	{
		anim = GetComponent<Animator> ();
		moveSpeed = 0.3f;
		moveSpeedY = 0.03f;
		xMin = -30f;
		xMax = 30f;
		yMin = -9.7f;
		yMax = 9.5f;
		movey = 1.5f;
		howManyY = (Random.Range (0f, 1f) * 100);
		howTimes = 0f;
		Boss = transform;
		GameObject helpVariable = GameObject.FindGameObjectWithTag ("Player");
		GameObject helpVariable2 = GameObject.FindGameObjectWithTag ("Player2");
		healthBar = transform.Find ("HealthBar").GetComponent<SpriteRenderer> ();
		healthScale = healthBar.transform.localScale;
		Player = helpVariable.transform;
		Player2 = helpVariable2.transform;
		HP = fullHP;
	}

	void FixedUpdate ()
	{
		distancePlayer = Vector2.Distance (transform.position, Player.position);
		distancePlayer2 = Vector2.Distance (transform.position, Player2.position);

		RaycastHit2D hit = Physics2D.CircleCast (transform.position, 1, new Vector2 (1, 0), 0, 1 << 15);
		if ((distancePlayer <= distancePlayer2) && Player.GetComponent<PlayerHealth> ().alive) {
			if ((Player.position.x <= Boss.position.x) & helpVariable2 == 0) {
				Flip ();
				helpVariable2++;
			} else if ((Player.position.x > Boss.position.x) & helpVariable2 != 0) {
				Flip ();
				helpVariable2 = 0;
			}
			if (distancePlayer > 3) {
				if (distancePlayer > 10)
					Boss.Translate (new Vector2 (((Player.position.x - transform.position.x) * moveSpeed) / 15, (Player.position.y - transform.position.y) * moveSpeedY) / 15);
				else
					Boss.Translate (new Vector2 (((Player.position.x - transform.position.x) * moveSpeed) / 8, (Player.position.y - transform.position.y) * moveSpeedY) / 8);
			}
		} else if ((distancePlayer > distancePlayer2) && Player2.GetComponent<PlayerHealth> ().alive) {
			if (Player2.GetComponent<PlayerHealth> ().alive && (Player2.position.x <= Boss.position.x) & helpVariable2 == 0) {
				Flip ();
				helpVariable2++;
			} else if ((Player2.position.x > Boss.position.x) & helpVariable2 != 0) {
				Flip ();
				helpVariable2 = 0;
			}
			if (distancePlayer2 > 3) {
				if (distancePlayer2 > 10)
					Boss.Translate (new Vector2 (((Player2.position.x - transform.position.x) * moveSpeed) / 15, (Player2.position.y - transform.position.y) * moveSpeedY) / 15);
				else
					Boss.Translate (new Vector2 (((Player2.position.x - transform.position.x) * moveSpeed) / 8, (Player2.position.y - transform.position.y) * moveSpeedY) / 8);
			}
		} else if (Player.GetComponent<PlayerHealth> ().alive) {
			if ((Player.position.x <= Boss.position.x) & helpVariable2 == 0) {
				Flip ();
				helpVariable2++;
			} else if ((Player.position.x > Boss.position.x) & helpVariable2 != 0) {
				Flip ();
				helpVariable2 = 0;
			}
			if (distancePlayer > 3) {
				if (distancePlayer > 10)
					Boss.Translate (new Vector2 (((Player.position.x - transform.position.x) * moveSpeed) / 15, (Player.position.y - transform.position.y) * moveSpeedY) / 15);
				else
					Boss.Translate (new Vector2 (((Player.position.x - transform.position.x) * moveSpeed) / 8, (Player.position.y - transform.position.y) * moveSpeedY) / 8);
			}
		} else {
			if (Player2.GetComponent<PlayerHealth> ().alive && (Player2.position.x <= Boss.position.x) & helpVariable2 == 0) {
				Flip ();
				helpVariable2++;
			} else if ((Player2.position.x > Boss.position.x) & helpVariable2 != 0) {
				Flip ();
				helpVariable2 = 0;
			}
			if (distancePlayer2 > 3) {
				if (distancePlayer2 > 10)
					Boss.Translate (new Vector2 (((Player2.position.x - transform.position.x) * moveSpeed) / 15, (Player2.position.y - transform.position.y) * moveSpeedY) / 15);
				else
					Boss.Translate (new Vector2 (((Player2.position.x - transform.position.x) * moveSpeed) / 8, (Player2.position.y - transform.position.y) * moveSpeedY) / 8);
			}
		}
			
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if (HP <= 0 && !dead) {
			// ... call the death function.
			moveSpeed = 0;
			moveSpeedY = 0;
			
			anim.SetTrigger ("Die");
			GetComponent<Collider2D> ().enabled = false;
			Invoke ("Death",2);
		}

		GetComponent<Rigidbody2D> ().position = new Vector2 (Mathf.Clamp (GetComponent<Rigidbody2D> ().position.x, xMin, xMax), Mathf.Clamp (GetComponent<Rigidbody2D> ().position.y, yMin, yMax));
	}

	public void Hurt (int attackPoints)
	{
		// Reduce the number of hit points by one.
		HP -= attackPoints;

		UpdateHealthBar ();
	}

	void Death ()
	{

		// Set dead to true.
		dead = true;

		GameManager.instance.deadEnemies++;
		GameManager.instance.points = GameManager.instance.points + GameManager.instance.multiplier;

		gameObject.GetComponent<Loot> ().generateBones (bonesValue);

		Destroy ();

		// Allow the enemy to rotate and spin it by adding a torque.
		//GetComponent<Rigidbody2D>().fixedAngle = false;
		//GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		// Play a random audioclip from the deathClips array.
		//int i = Random.Range(0, deathClips.Length);
		//AudioSource.PlayClipAtPoint(deathClips[i], transform.position);
		//Invoke ("Destroy",deathClips[i].length+1.0f);

	}

	void Destroy ()
	{
		Destroy (gameObject);
	}

	public void Flip ()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
	
	public void UpdateHealthBar ()
	{
		healthBar.material.color = Color.Lerp (Color.green, Color.red, 1 - HP * 0.001f);
		healthBar.transform.localScale = new Vector3 (healthScale.x * HP * 0.001f, 1, 1);
	}
}
