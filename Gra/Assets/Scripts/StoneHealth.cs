using UnityEngine;
using System.Collections;

public class StoneHealth : MonoBehaviour
{
	public int fullHealth = 25;
	public int stoneHealth = 25;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//transform.Rotate(Vector3.forward, Time.deltaTime*15, Space.World);
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if ((col.gameObject.tag == "Enemy" && !col.gameObject.GetComponent<Enemy>().dead) || 
		    (col.gameObject.tag == "Enemy2" && !col.gameObject.GetComponent<Enemy2>().dead) || 
		    (col.gameObject.tag == "Boss" && !col.gameObject.GetComponent<EnemyBoss>().dead)) {
			if(stoneHealth >= 5) {
				stoneHealth -= 5;
			}
			if (stoneHealth == 0) {
				Destroy (gameObject);
			}
		}
	}
}

