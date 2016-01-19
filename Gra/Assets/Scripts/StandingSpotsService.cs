using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum SpotType
{
	BonusBones,
	DeadlyRock,
	SpikeTraps,
}
	;

public class StandingSpotsService : MonoBehaviour
{
	public SpotType currentSpotType = SpotType.BonusBones;
	public GameObject spot;
	public GameObject spikeTrap;
	public GameObject deadlyRock;
	public bool bothStanding;
	public bool shouldCheck;
	public bool shouldRandSpotType;
	public float timeToGain;
	public float timeToSpawnNext;
	public float minValueToRand = 4.0f;
	public float maxValueToRand = 10.0f;
	private GameObject spot1;
	private GameObject spot2;
	public float timer;
	public float overallTimer;
	public float firstStarPositionXToRand = 0.0f;
	public float lastStarPositionXToRand = 15.0f;
	public float yForFirstSpot = -5.0f;
	public float yForSecondSpot = 5.0f;
	public Vector2 startPositionToSpawnSpikes;
	public Vector2 directionToSpawnSpikes;
	public Vector2 deadlyRockSpawnPosition;
	public Vector2 deadlyRockVelocity = new Vector2 (0.0f, -5.0f);

	// Use this for initialization
	void Start ()
	{
		overallTimer = 0.0f;
		timeToSpawnNext = Random.Range (minValueToRand - GameManager.instance.spotsWaitTime, maxValueToRand - GameManager.instance.spotsWaitTime);
		timer = 0.0f;
		shouldCheck = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.instance.actualGameState == gameState.Play) {
			if (!shouldCheck)
				overallTimer += Time.deltaTime;

			if (overallTimer >= timeToSpawnNext) {
				SpawnSpots ();
				overallTimer = 0.0f;
				timeToSpawnNext = Random.Range (minValueToRand, maxValueToRand);
			}

			if (currentSpotType == SpotType.SpikeTraps) {
				
				if (timer >= timeToGain) {
					PlantSpikeTraps ();
					Destroy (spot1);
					Destroy (spot2);
					shouldCheck = false;
				}
				if (shouldCheck && spot1.GetComponent<StandingSpotScript> ().activate && spot2.GetComponent<StandingSpotScript> ().activate) {
					bothStanding = true;
					GameManager.instance.notifyTxt.GetComponent<Text> ().text = "TIME TO PLANT SPIKE TRAPS : " + Mathf.Abs (Mathf.FloorToInt (timeToGain - timer)).ToString () + " s";
					timer += Time.deltaTime;
				} else {
					bothStanding = false;
					timer = 0.0f;
				}
			}

			if (currentSpotType == SpotType.DeadlyRock) {
			
				if (timer >= timeToGain) {
					ThrowDeadlyRock ();
					Destroy (spot1);
					Destroy (spot2);
					shouldCheck = false;
				}
				if (shouldCheck && spot1.GetComponent<StandingSpotScript> ().activate && spot2.GetComponent<StandingSpotScript> ().activate) {
					bothStanding = true;
					GameManager.instance.notifyTxt.GetComponent<Text> ().text = "TIME TO THROW DEADLY FOR ENEMIES ROCK : " + Mathf.Abs (Mathf.FloorToInt (timeToGain - timer)).ToString () + " s";
					timer += Time.deltaTime;
				} else {
					bothStanding = false;
					timer = 0.0f;
				}
			}

			if (currentSpotType == SpotType.BonusBones) {

				if (timer >= timeToGain) {
					GameManager.instance.bones += 20;
					Destroy (spot1);
					Destroy (spot2);
					shouldCheck = false;
				}
				if (shouldCheck && spot1.GetComponent<StandingSpotScript> ().activate && spot2.GetComponent<StandingSpotScript> ().activate) {
					bothStanding = true;
					GameManager.instance.notifyTxt.GetComponent<Text> ().text = "TIME TO GAIN BONES : " + Mathf.Abs (Mathf.FloorToInt (timeToGain - timer)).ToString () + " s";
					timer += Time.deltaTime;
				} else {
					bothStanding = false;
					timer = 0.0f;
				}
			}
		}
	}

	void SpawnSpots ()
	{
		if (shouldRandSpotType)
			currentSpotType = GameManager.GetRandomEnum<SpotType> ();

		spot1 = Instantiate (spot, new Vector3 (Random.Range (firstStarPositionXToRand, lastStarPositionXToRand), yForFirstSpot), transform.rotation) as GameObject;
		spot2 = Instantiate (spot, new Vector3 (Random.Range (firstStarPositionXToRand, lastStarPositionXToRand), yForSecondSpot), transform.rotation) as GameObject;

		if (currentSpotType == SpotType.SpikeTraps) {
			spot1.GetComponent<StandingSpotScript> ().color = Color.yellow;
			spot2.GetComponent<StandingSpotScript> ().color = Color.yellow;
		} else if (currentSpotType == SpotType.BonusBones) {
			spot1.GetComponent<StandingSpotScript> ().color = Color.grey;
			spot2.GetComponent<StandingSpotScript> ().color = Color.grey;
		} else if (currentSpotType == SpotType.DeadlyRock) {
			spot1.GetComponent<StandingSpotScript> ().color = new Color (1.0f, 0.5f, 0.5f);
			spot2.GetComponent<StandingSpotScript> ().color = new Color (1.0f, 0.5f, 0.5f);
		}

		shouldCheck = true;
	}

	void PlantSpikeTraps ()
	{
		
		var spikeTraps = GameObject.FindGameObjectsWithTag ("SpikeTrap");
		foreach (var t in spikeTraps)
			Destroy (t);  					//Destroying old traps


		Vector3 tmpVect = new Vector3 (startPositionToSpawnSpikes.x, startPositionToSpawnSpikes.y, 0);
		for (int i = 0; i<5; i++) {
			Instantiate (spikeTrap, tmpVect, Quaternion.Euler(0, 0, 45));
			tmpVect.y += (directionToSpawnSpikes.y * 3.0f);
			tmpVect.x += (directionToSpawnSpikes.x * 3.0f);
		}
	}

	void ThrowDeadlyRock ()
	{
		Rigidbody2D deadlyRockInstance = Instantiate (deadlyRock.GetComponent<Rigidbody2D> (), deadlyRockSpawnPosition, Quaternion.identity) as Rigidbody2D;
		deadlyRockInstance.velocity = deadlyRockVelocity;
	}
}
