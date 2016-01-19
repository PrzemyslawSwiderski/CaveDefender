using UnityEngine;
using System.Collections;

public class SpikeTrapScript : MonoBehaviour
{
	public int attackPoints = 1000;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//transform.Rotate(Vector3.forward, Time.deltaTime*15, Space.World);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if ((col.tag == "Enemy" && !col.GetComponent<Enemy>().dead) || (col.tag == "Enemy2" && !col.GetComponent<Enemy2>().dead) || 
		    (col.tag == "Boss" && !col.GetComponent<EnemyBoss>().dead)) {
			// ... find the Enemy script and call the Hurt function.
			if (col.tag == "Enemy")
				col.gameObject.GetComponent<Enemy> ().Hurt (attackPoints);
			else if (col.tag == "Enemy2")
				col.gameObject.GetComponent<Enemy2> ().Hurt (attackPoints);
			Destroy (gameObject);
		}
	}
}

