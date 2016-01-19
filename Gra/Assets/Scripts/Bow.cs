using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour
{
	public Rigidbody2D arrow;				
	public float speed = 20f;				// The speed the rocket will fire at.
	public string fireButton = "Enter Fire Input";
	public float bowSpeed;
	public GameObject trap1;
	public GameObject trap2;
	private float reloadTime;
	private float timeToSpawnNextTrap = 6;
	private float timer;
	private EasyMovement playerMovement;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.


	void Awake ()
	{
		reloadTime = 0.0f;
		anim = transform.root.gameObject.GetComponent<Animator> ();
		playerMovement = transform.root.GetComponent<EasyMovement> ();
	}

	void Update ()
	{
		bowSpeed = GameManager.instance.bowSpeed;
		reloadTime += Time.deltaTime;
		timer += Time.deltaTime;
		// If the fire button is pressed...
		if (Input.GetButtonDown (fireButton) && reloadTime>=bowSpeed) {

			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger ("Shoot");
			// If the player is facing right...
			if (playerMovement.facingRight) {

				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate (arrow, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (speed, 0);	
				if ( timer >  timeToSpawnNextTrap && bowSpeed < 0.3f ){
				Instantiate (trap1, new Vector2(transform.position.x - 2f,transform.position.y), transform.rotation);
				timer = 0;
				}

			
			} else {
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate (arrow, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (-speed, 0);
				if ( timer >  timeToSpawnNextTrap && bowSpeed < 0.3f ){
					Instantiate (trap2, new Vector2(transform.position.x - 4f,transform.position.y), transform.rotation);
					timer = 0;
				}
			}
			reloadTime = 0.0f;
		}

	}
}
