using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LayTrap : MonoBehaviour
{

	public int trapCost = 10;
	public GameObject trap;				// Prefab of the trap.
	public string plantButton = "Enter Fire Input";
	public float timer;
	public float timeToPlant;

	void Start ()
	{
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton(plantButton) && (GameManager.instance.bones - trapCost) >= 0) {
			timer+=Time.deltaTime;
			GameManager.instance.notifyTxt.GetComponent<Text> ().text = "TIME TO PLANT TRAP : " + Mathf.Abs (Mathf.FloorToInt (timeToPlant - timer)).ToString () + " s";
			if (timer >= timeToPlant) {
				Instantiate (trap, transform.position, transform.rotation);
				GameManager.instance.bones = GameManager.instance.bones - trapCost;
				timer = 0.0f;
			}
		}
	}
}
