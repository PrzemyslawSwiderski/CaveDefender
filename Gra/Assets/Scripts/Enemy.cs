using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float
		moveSpeed, moveSpeedY;		// The speed the enemy moves at.
	public int
		HP = 2;                 // How many times the enemy can be hit before it dies.
	public int bonesValue = 5;
	public bool dead = false;           // Whether or not the enemy is dead.
	public bool changeDirection;
	public bool ifRandomSpeedX;
	public float xMin, xMax, yMin, yMax, movey, howManyY, howTimes;
	private Animator anim;						// Reference to the Animator on the player
	private Transform Cave;

	void Awake ()
	{
		if (ifRandomSpeedX) {
			if (changeDirection)
				moveSpeed = -(Random.Range (0.1f, 1f) * 2);
			else
				moveSpeed = (Random.Range (0.1f, 1f) * 2);
		}

		moveSpeedY = (Random.Range (-1f, 1f) * 2) + 1.1f;
		xMin = -30f;
		xMax = 30f;
		yMin = -9.7f;
		yMax = 9.5f;
		movey = 1.5f;
		howManyY = (Random.Range (0f, 1f) * 100);
		howTimes = 0f;
		GameObject helpVariable = GameObject.FindGameObjectWithTag ("Cavenew");
		
		anim = GetComponent<Animator> ();
		Cave = helpVariable.transform;
	}

	void FixedUpdate ()
	{
		RaycastHit2D hit = Physics2D.CircleCast (transform.position, 1, new Vector2 (1, 0), 0, 1 << 15);
		if (!dead) {
			if (((GetComponent<Rigidbody2D> ().position.y + movey) <= yMax) & ((GetComponent<Rigidbody2D> ().position.y + movey) >= yMin) & (howTimes <= howManyY)) {
				howTimes = howTimes + 1;
			} else {
				howTimes = 0;
				moveSpeedY = -moveSpeedY;
			}
			if (GetComponent<Rigidbody2D> ().position.x < 20.0f) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.localScale.x * moveSpeed, transform.localScale.y * moveSpeedY);
			} else {
				moveSpeedY = Mathf.Abs (moveSpeedY);
				GetComponent<Rigidbody2D> ().velocity = (new Vector2 (((Cave.position.x - transform.position.x) * Mathf.Abs (moveSpeed)) / 15, (((Cave.position.y - transform.position.y) * moveSpeedY) / 15)));

			}
		}

		// If the enemy has zero or fewer hit points and isn't dead yet...
		if (HP <= 0 && !dead) {
			// ... call the death function.
			moveSpeed = 0;
			moveSpeedY = 0;
			anim.SetTrigger ("Die");
			GetComponent<Collider2D> ().enabled = false;
			dead = true;

			Debug.Log ("1Current animator clip length " + anim.GetCurrentAnimatorStateInfo (0).length);
			Debug.Log ("2Current animator clip length " + anim.GetCurrentAnimatorClipInfo (0) [0].clip.length);
			Invoke ("Death", anim.GetCurrentAnimatorStateInfo (0).length);
		}
        
		GetComponent<Rigidbody2D> ().position = new Vector2 (Mathf.Clamp (GetComponent<Rigidbody2D> ().position.x, xMin, xMax), Mathf.Clamp (GetComponent<Rigidbody2D> ().position.y, yMin, yMax));
	}

	public void Hurt (int attackPoints)
	{
		// Reduce the number of hit points by one.
		HP -= attackPoints;
		movey *= Random.Range (0, 1) * 2 - 1;
		if (((GetComponent<Rigidbody2D> ().position.y + movey) <= yMax) & ((GetComponent<Rigidbody2D> ().position.y + movey) >= yMin))
			transform.Translate (0, movey, 0);
		else
			transform.Translate (0, -movey, 0);
	}

	public void Death ()
	{
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
		enemyScale.y *= -1;
		transform.localScale = enemyScale;
	}
}
