using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour
{
	public int attackPoints = 10;
	public string fireButton = "Enter Fire Input";
	private Animator anim;					// Reference to the Animator component.
	private CircleCollider2D spearPeak;


	// Use this for initialization
	void Start ()
	{
		spearPeak = gameObject.GetComponent<CircleCollider2D> ();
		anim = transform.root.gameObject.GetComponent<Animator> ();
		spearPeak.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		attackPoints = GameManager.instance.spearDamage;
		// If the fire button is pressed...
		if (Input.GetButtonDown (fireButton)) {
			spearPeak.enabled = true;
			anim.SetTrigger ("Hit");
			spearPeak.isTrigger = true;
			Invoke ("DisableByTime", 0.3F);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		// If it hits an enemy...
		if (col.tag == "Enemy" || col.tag == "Enemy2" || col.tag == "Boss") {
			// ... find the Enemy script and call the Hurt function.
			if (col.tag == "Enemy")
				col.gameObject.GetComponent<Enemy> ().Hurt (attackPoints);
			else if (col.tag == "Enemy2")
				col.gameObject.GetComponent<Enemy2> ().Hurt (attackPoints);
			else if (col.tag == "Boss")
				col.gameObject.GetComponent<EnemyBoss> ().Hurt (attackPoints);
		}
	}

	void DisableByTime ()
	{
		spearPeak.enabled = false;
		spearPeak.isTrigger = false;
	}
}
