using UnityEngine;
using System.Collections;

public enum Weapon
{
	Spear,
	Bow,
	MudTrap,
	SpikeTrap,
	Stone
}
;

public class ChangeWeapon : MonoBehaviour
{
	public KeyCode changeKey;
	public Weapon activeWeapon;
	private GameObject spear;
	private GameObject bow;
	private GameObject mudTrap;
	private GameObject spikeTrap;
	private GameObject stone;

	// Use this for initialization
	void Start ()
	{
		activeWeapon = Weapon.Spear;
		spear = transform.Find ("RightArm").transform.Find ("Spear").gameObject;
		bow = transform.Find ("RightArm").transform.Find ("Bow").gameObject;
		mudTrap = transform.Find ("MudTrap").gameObject;
		spikeTrap = transform.Find ("SpikeTrap").gameObject;
		stone = transform.Find ("Stone").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (changeKey)) {
			if (activeWeapon == Weapon.Spear)
				activeWeapon = Weapon.Bow;
			else if (activeWeapon == Weapon.Bow)
				activeWeapon = Weapon.MudTrap;
			else if (activeWeapon == Weapon.MudTrap)
				activeWeapon = Weapon.SpikeTrap;
			else if (activeWeapon == Weapon.SpikeTrap)
				activeWeapon = Weapon.Stone;
			else if (activeWeapon == Weapon.Stone)
				activeWeapon = Weapon.Spear;
		}

		if (activeWeapon == Weapon.Spear) {
			spikeTrap.SetActive (false);
			bow.SetActive (false);
			mudTrap.SetActive (false);
			spear.SetActive (true);
			stone.SetActive (false);
		}

		if (activeWeapon == Weapon.Bow) {
			spikeTrap.SetActive (false);
			spear.SetActive (false);
			mudTrap.SetActive (false);
			bow.SetActive (true);
			stone.SetActive (false);
		}
		
		if (activeWeapon == Weapon.MudTrap) {
			spear.SetActive (false);
			spikeTrap.SetActive (false);
			bow.SetActive (false);
			mudTrap.SetActive (true);
			stone.SetActive (false);
		}
		
		if (activeWeapon == Weapon.SpikeTrap) {
			spear.SetActive (false);
			spikeTrap.SetActive (true);
			bow.SetActive (false);
			mudTrap.SetActive (false);
			stone.SetActive (false);
		}
		
		if (activeWeapon == Weapon.Stone) {
			spear.SetActive (false);
			spikeTrap.SetActive (false);
			bow.SetActive (false);
			mudTrap.SetActive (false);
			stone.SetActive (true);
		}
	}
}

