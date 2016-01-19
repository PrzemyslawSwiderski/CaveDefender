using UnityEngine;
using System.Collections;

public class StandingSpotScript : MonoBehaviour
{
	public Color color = Color.white;
	public bool activate;

	// Use this for initialization
	void Start ()
	{
		activate = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (activate)
			gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		else
			gameObject.GetComponent<SpriteRenderer> ().color = color;
	}

	void OnTriggerStay2D (Collider2D col)
	{	
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
			activate = true;
	}
	
	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
			activate = false;
	}
}
