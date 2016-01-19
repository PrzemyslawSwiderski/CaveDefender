using UnityEngine;
using System.Collections;

public class TrapsService : MonoBehaviour
{
	public float slowMultiplier = 2.0F;
	private float oldMoveSpeed = 0.0F;
	private Collider2D lastCollider;

	// Use this for initialization
	void Start ()
	{
		oldMoveSpeed = gameObject.GetComponent<Enemy> ().moveSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(lastCollider && !lastCollider.isActiveAndEnabled)
			gameObject.GetComponent<Enemy> ().moveSpeed = oldMoveSpeed;
	}

	void OnTriggerStay2D (Collider2D col)
	{
		lastCollider = col;
		if (col.tag == "MudTrap") {
			gameObject.GetComponent<Enemy> ().moveSpeed = oldMoveSpeed / slowMultiplier;
		}
	}
	void OnTriggerExit2D (Collider2D col)
	{
		lastCollider = col;
		if (col.tag == "MudTrap")
		gameObject.GetComponent<Enemy> ().moveSpeed = oldMoveSpeed;
	}
}
