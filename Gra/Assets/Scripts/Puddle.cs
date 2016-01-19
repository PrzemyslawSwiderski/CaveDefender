using UnityEngine;
using System.Collections;

public class Puddle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Player2"  )
			coll.gameObject.GetComponent<EasyMovement>().Speed = 3f;
		
	}
	void OnTriggerExit2D(Collider2D coll) {
		
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Player2" )
			coll.gameObject.GetComponent<EasyMovement>().Speed = 10f;
		
	}
}