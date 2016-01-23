using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{

	public Text deadEnemiesTxt; // assign it from inspector
	public Text bonesTxt; // assign it from inspector
	public Text stonesTxt;
	public Text stonesInfo1;
	public Text stonesInfo2;
	public Image stonesInfo3;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		bonesTxt.text = GameManager.instance.bones.ToString ();
		deadEnemiesTxt.text = GameManager.instance.deadEnemies.ToString ();
		stonesTxt.text = (GameManager.instance.buildStonesToBuildTower - GameManager.instance.buildStones).ToString ();
		if (GameManager.instance.towerBuilded) {
			stonesInfo1.enabled= false;
			stonesInfo2.enabled= false;
			stonesInfo3.enabled= false;
		}
	}
}
