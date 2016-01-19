using UnityEngine;
using System.Collections;

public class TrapDestroyer : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{
		float startDestroyDelay = GameManager.instance.trapsDuration;
		Invoke("DisableBeforeDestroy",startDestroyDelay);
		Invoke("DestroyGameObject",startDestroyDelay+0.1F);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void DestroyGameObject ()
	{
		// Destroy this gameobject, this can be called from an Animation Event.
		Destroy (gameObject);
	}
	void DisableBeforeDestroy()
	{
		gameObject.SetActive (false);
	}
}

