using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.
	public float scaleMultiplier = 0.2f;
	public float xOffset = 0.0f;
	public float yOffset = 0.0f;
	private float cameraSize;
	private Camera camera;
	private GameObject player1;		// Reference to the player's transform.
	private GameObject player2;		// Reference to the player's transform.

	void Awake ()
	{
		camera = gameObject.GetComponent<Camera> ();
		cameraSize = camera.orthographicSize;
		player1 = GameObject.FindGameObjectWithTag ("Player");
		player2 = GameObject.FindGameObjectWithTag ("Player2");
	}

	void FixedUpdate ()
	{
		TrackPlayer ();
		AdjustSize ();
	}
	
	void TrackPlayer ()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		
		targetX = Mathf.Lerp (transform.position.x, (player1.transform.position.x + player2.transform.position.x) / 2, xSmooth * Time.deltaTime);
		targetY = Mathf.Lerp (transform.position.y, (player1.transform.position.y + player2.transform.position.y) / 2, ySmooth * Time.deltaTime);

//		if (player1) {
//			if (CheckXMargin ())
//				targetX = Mathf.Lerp (transform.position.x, player1.transform.position.x, xSmooth * Time.deltaTime);
//			
//			if (CheckYMargin ())
//				targetY = Mathf.Lerp (transform.position.y, player1.transform.position.y, ySmooth * Time.deltaTime);
//		}
		targetX = Mathf.Clamp (targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp (targetY, minXAndY.y, maxXAndY.y);

		transform.position = new Vector3 (targetX + xOffset, targetY + yOffset, transform.position.z);
	}

	void AdjustSize ()
	{
		float distance = Vector2.Distance (player1.transform.position, player2.transform.position);

		camera.orthographicSize = cameraSize + (distance) * scaleMultiplier;
	}
}
