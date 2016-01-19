using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

	public Text bonesTxt; // assign it from inspector,
	public Text costTxt; // assign it from inspector,
	public Text speedLvlTxt; // assign it from inspector,
	public Text damageLvlTxt; // assign it from inspector,
	public Text waitTimeLvlTxt; // assign it from inspector,

	public Text durationLvlTxt; // assign it from inspector,
	public Text speedUpgradeTxt; // assign it from inspector,
	public Text damageUpgradeTxt; // assign it from inspector,
	public Text durationUpgradeTxt; // assign it from inspector,
	public Text waitingTimeUpgradeTxt; // assign it from inspector,
	public Text caveHpUpgradeTxt; // assign it from inspector,



	public int speedLvl;
	public int damageLvl;
	public int durationLvl;
	public int waitTimeLvl;
	public int bowUpgradeCost;
	public int spearUpgradeCost;
	public int trapsUpgradeCost;
	public int waitTimeUpgradeCost;
	public int caveHpUpgradeCost;

	// Use this for initialization
	void Start ()
	{
		speedLvl = 0;
		durationLvl = 0;
		damageLvl = 0;
		waitTimeLvl = 0;
		spearUpgradeCost = 5;
		bowUpgradeCost = 5;
		trapsUpgradeCost = 5;
		waitTimeUpgradeCost = 5;
		caveHpUpgradeCost = 50;
	}
	
	// Update is called once per frame
	void Update ()
	{
		costTxt.text = bowUpgradeCost.ToString ();
		bonesTxt.text = GameManager.instance.bones.ToString ();
		speedLvlTxt.text = speedLvl.ToString ();
		damageLvlTxt.text = damageLvl.ToString ();
		durationLvlTxt.text = durationLvl.ToString ();
		waitTimeLvlTxt.text = waitTimeLvl.ToString ();

		speedUpgradeTxt.text = bowUpgradeCost.ToString ();
		damageUpgradeTxt.text = spearUpgradeCost.ToString ();
		durationUpgradeTxt.text = trapsUpgradeCost.ToString ();
		waitingTimeUpgradeTxt.text = waitTimeUpgradeCost.ToString ();
		caveHpUpgradeTxt.text = caveHpUpgradeCost.ToString ();
	}

	public void UpgradeBowSpeed ()
	{
		if (GameManager.instance.bones - bowUpgradeCost >= 0 && GameManager.instance.bowSpeed - 0.15f > 0) {
			GameManager.instance.bowSpeed -= 0.15f;
			speedLvl++;
			GameManager.instance.bones -= bowUpgradeCost;
			bowUpgradeCost += 5;
		}
	}
	
	public void UpgradeSpearDamage ()
	{
		if (GameManager.instance.bones - spearUpgradeCost >= 0) {
			GameManager.instance.spearDamage += 2;
			damageLvl++;
			GameManager.instance.bones -= spearUpgradeCost;
			spearUpgradeCost += 5;
		}
	}
	
	public void UpgradeTrapsDuration ()
	{
		if (GameManager.instance.bones - trapsUpgradeCost >= 0) {
			GameManager.instance.trapsDuration *= 2.0f;
			durationLvl++;
			GameManager.instance.bones -= trapsUpgradeCost;
			trapsUpgradeCost += 5;
		}

	}

	public void UpgradeWaitTime ()
	{
		if (GameManager.instance.bones - waitTimeUpgradeCost >= 0) {
			GameManager.instance.spotsWaitTime += 0.2f;
			waitTimeLvl++;
			GameManager.instance.bones -= waitTimeUpgradeCost;
			waitTimeUpgradeCost += 5;
		}
		
	}

	public void UpgradeCaveHP ()
	{
		var cave = GameObject.FindGameObjectWithTag ("Cavenew").GetComponent<CaveHealth> ();

		if (GameManager.instance.bones - caveHpUpgradeCost >= 0 && cave.health < 99) {
			cave.AddHp(1);
			GameManager.instance.bones -= caveHpUpgradeCost;
		}
		
	}

}
