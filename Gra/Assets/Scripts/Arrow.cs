using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
	public int attackPoints = 10;

	void Start ()
	{
		Destroy (gameObject, 10);
	}

	void Update ()
	{
		attackPoints = GameManager.instance.arrowDamage;
	}

	void OnTriggerEnter2D (Collider2D col)
	{

		// If it hits an enemy...
		if (col.tag == "Enemy" || col.tag == "Enemy2" || col.tag == "Boss") {
			if (col.tag == "Enemy")
				col.gameObject.GetComponent<Enemy> ().Hurt (attackPoints);
			else if (col.tag == "Enemy2")
				col.gameObject.GetComponent<Enemy2> ().Hurt (attackPoints);
			else if (col.tag == "Boss")
				col.gameObject.GetComponent<EnemyBoss> ().Hurt (attackPoints);

			Destroy (gameObject);
		}
	}

}

