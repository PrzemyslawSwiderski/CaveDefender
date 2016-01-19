using UnityEngine;
using System.Collections;

public class ArrowTrap : MonoBehaviour {

	int attackPoints = 10;
	public float TimeAlive = 1f;
	void Start ()
	{

		Destroy (gameObject, 2f);
	}

	void FixedUpdate(){
			gameObject.transform.eulerAngles = new Vector3(
			gameObject.transform.eulerAngles.x,
			gameObject.transform.eulerAngles.y,
			90
			);

		if (gameObject != null) GetComponent<Rigidbody2D>().velocity = new Vector2(0, transform.localScale.y * 10);
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		

		if (col.tag == "Player") {
			// ... find the Enemy script and call the Hurt function.
			if (col.tag == "Player")
				col.gameObject.GetComponent<PlayerHealth> ().TakeDamageFromArrow (20);
			else
				col.gameObject.GetComponent<PlayerHealth> ().TakeDamageFromArrow (20);
			Destroy (gameObject);
		}
		

	}


}
