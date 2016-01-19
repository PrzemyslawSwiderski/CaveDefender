using UnityEngine;
using System.Collections;

public class DeadlyRock : MonoBehaviour
{
	public int hurtEnemies;
	public int maxHurtEnemies = 5;
	public int attackPoints = 20;

	// Use this for initialization
	void Start ()
	{
		hurtEnemies = 0;
		Destroy (gameObject, 100);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (Vector3.back, Time.deltaTime * 30, Space.World);
		if (hurtEnemies >= maxHurtEnemies)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		// If it hits an enemy...
		if (col.tag == "Enemy" || col.tag == "Enemy2" || col.tag == "Boss") {
			hurtEnemies++;
			if (col.tag == "Enemy")
				col.gameObject.GetComponent<Enemy> ().Hurt (attackPoints);
			else if (col.tag == "Enemy2")
				col.gameObject.GetComponent<Enemy2> ().Hurt (attackPoints);
			else if (col.tag == "Boss")
				col.gameObject.GetComponent<EnemyBoss> ().Hurt (attackPoints);
		}
	}
}

