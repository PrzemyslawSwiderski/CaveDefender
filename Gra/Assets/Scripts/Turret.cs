using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	int attackPoints = 10;
	public float TimeAlive = 1f;
	void Start ()
	{
		Destroy (gameObject, 2f);
		//transform.position = new Vector3(25.19f + 0.001f, -10.09f + 0.001f, 0);
		transform.localEulerAngles = new Vector3 (0, 0, 150);

	}



	void FixedUpdate(){
		if (GameManager.instance.towerBuilded) {
			GameObject turret = GameObject.Find("turret");
			GameObject arrow = GameObject.Find("arrow 2");


	/*	Debug.Log(gameObject.GetType ());
		turret.transform.eulerAngles = new Vector3(
			turret.transform.eulerAngles.x,
			turret.transform.eulerAngles.y,
			90
			);*/
		
			if (gameObject != null && GameManager.instance.towerBuilded) {


				GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * -20.8f, transform.localScale.y * 10);

				//GetComponent<Rigidbody2D> ().velocity = new Vector2 (25.19f, 10.09f * 10);
			}
		}
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		
		
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
