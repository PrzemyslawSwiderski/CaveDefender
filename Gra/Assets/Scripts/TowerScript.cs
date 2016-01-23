using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour
{
	public bool turretMiddleBuilt;
	public GameObject turretStart;
	public GameObject turretMiddle;
	public GameObject turretEnd;
	public float spawnDelay;
	public float spawnRepeatRate;
	float xToRand=-16;
	float yToRand=10;

	public Rigidbody2D arrow;				

	// Use this for initialization
	void Start ()
	{
		Instantiate (turretStart, transform.position, transform.rotation);
		
		InvokeRepeating ("SpawnArrow", spawnDelay, spawnRepeatRate);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.instance.buildStones > (GameManager.instance.buildStonesToBuildTower/2) && !turretMiddleBuilt) {
			Instantiate (turretMiddle, transform.position, transform.rotation);
			turretMiddleBuilt=true;
		}
		
		if (GameManager.instance.buildStones > GameManager.instance.buildStonesToBuildTower) {
			Instantiate (turretEnd, transform.position, transform.rotation);
			GameManager.instance.towerBuilded = true;
			GameManager.instance.buildStones = 0;
		}
	}
	void SpawnArrow()
	{
		if (GameManager.instance.towerBuilded) {
			yToRand=Random.Range(0,20);
			Rigidbody2D bulletInstance = Instantiate (arrow, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.Euler (new Vector3 (0, 0, Vector2.Angle(new Vector2(0,1),new Vector2(xToRand,yToRand))+90))) as Rigidbody2D;
			//bulletInstance.velocity = new Vector2(transform.localScale.x * -20.8f, transform.localScale.y * 10);
			bulletInstance.velocity = new Vector2(xToRand, yToRand);
			//bulletInstance.velocity =Quaternion.AngleAxis(xToRand, Vector3.up).eulerAngles;

		}
	}
}

