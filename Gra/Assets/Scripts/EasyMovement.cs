using UnityEngine;
using System.Collections;

public class EasyMovement : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	
	public float Speed = 10f;
	public string HorizontalInput = "empty";
	public string VerticalInput = "empty";
	private float movex = 0f;
	private float movey = 0f;
	private Animator anim;					// Reference to the player's animator component.

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		//Instantiate (turretStart, new Vector2 (25.19f, -9.0f), transform.rotation);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	void FixedUpdate ()
	{
		movex = Input.GetAxis (HorizontalInput);
		movey = Input.GetAxis (VerticalInput);

		anim.SetFloat("Speed", (Mathf.Abs(movex)+Mathf.Abs(movey))/2);

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (movex * Speed, movey * Speed);
		
	
		// If the input is moving the player right and the player is facing left...
		if (movex > 0 && !facingRight)
			// ... flip the player.
			Flip ();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (movex < 0 && facingRight)
			// ... flip the player.
			Flip ();
	}	

	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}

