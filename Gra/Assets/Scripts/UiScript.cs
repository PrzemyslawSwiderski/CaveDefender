using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{

	public Text deadEnemiesTxt; // assign it from inspector
	public Text bonesTxt; // assign it from inspector
	public Text Points;
	public Text stonesTxt;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		bonesTxt.text = GameManager.instance.bones.ToString ();
		deadEnemiesTxt.text = GameManager.instance.deadEnemies.ToString ();
		Points.text = "Points: " + GameManager.instance.points.ToString ();
		stonesTxt.text = GameManager.instance.buildStones.ToString ();
	}
}
