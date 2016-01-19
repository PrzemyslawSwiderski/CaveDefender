using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum gameState
{
	RoundStart,
	Play,
	GameOver,
	Shop,
	Win
}
;

public class GameManager : MonoBehaviour
{
	public static GameManager instance; //Singleton pattern
	public gameState actualGameState;
	public int deadEnemies;
	public int points;
	public int multiplier = 3;
	public int bones;
	public int buildStones;
	public bool towerBuilded = false;
	public int actualRound = 1;

	//variables needed by shop
	public int arrowDamage;
	public int spearDamage;
	public float bowSpeed;
	public float trapsDuration;
	public float timeToStart;
	public float spotsWaitTime;
	public GameObject helpScreen;
	public GameObject shop;
	public GameObject gui;
	public GameObject notifyTxt;
	public GameObject enterInfo;
	public GameObject spawner1;
	public GameObject spawner2;
	public GameObject spawner3;
	public GameObject spawnerB;
	public GameObject enemy1;
	public GameObject dinoEnemy;
	public GameObject boss;
	public int[] deadEnemiesForEachRound = {5,10,20,1,15,20,30,3};


	// Use this for initialization
	void Start ()
	{
		instance = this;  	//Singleton pattern, always first
		actualGameState = gameState.RoundStart;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape))
			Application.LoadLevel (0);
		
		if (actualGameState == gameState.Shop) {
			helpScreen.SetActive (false);
			gui.SetActive (false);
			shop.SetActive (true);

			Pauser.paused = true;

			if (Input.GetKeyUp (KeyCode.Return)) {
				Pauser.paused = false;
				actualGameState = gameState.RoundStart;
			}
		}
		if (actualGameState == gameState.RoundStart) {
			ClearRound ();
			
			helpScreen.SetActive (true);
			gui.SetActive (false);
			shop.SetActive (false);


			Pauser.paused = true;
			deadEnemies = 0;
			points = 0;
			timeToStart = 3.0f;
			enterInfo.GetComponent<Text> ().text = "Press Enter To Start Round Number: " + actualRound.ToString ();

			if (Input.GetKeyUp (KeyCode.Return)) {
				Pauser.paused = false;
				actualGameState = gameState.Play;
			}
		}

		if (actualGameState == gameState.Play) {
			helpScreen.SetActive (false);
			gui.SetActive (true);
			shop.SetActive (false);


			if (timeToStart > 0.0f) {
				if (Input.GetKeyUp (KeyCode.Return)) {
					//timeToStart = 0;
				}
				timeToStart -= Time.deltaTime;
				if (actualRound == 4 || actualRound == 8)
					notifyTxt.GetComponent<Text> ().text = "TIME TO START ROUND WITH BOSS!!! " + " : " + Mathf.Abs (Mathf.Round (timeToStart)).ToString () + " s";
				else
					notifyTxt.GetComponent<Text> ().text = "TIME TO START ROUND " + actualRound.ToString () + " : " + Mathf.Abs (Mathf.Round (timeToStart)).ToString () + " s";
			
			} else {
				notifyTxt.GetComponent<Text> ().text = "";
				switch (actualRound) {
				case 1:
					Round1 ();
					break;
				case 2:
					Round2 ();
					break;
				case 3:
					Round3 ();
					break;
				case 4:
					BossRound ();
					break;
				case 5:
					Round5 ();
					break;
				case 6:
					Round6 ();
					break;
				case 7:
					Round7 ();
					break;
				case 8:
					BossRound2 ();
					break;
				default:
					break;
				}
			}
			if (Input.GetKeyUp (KeyCode.P)) {
				Pauser.paused = !Pauser.paused;
				if (Pauser.paused)
					notifyTxt.GetComponent<Text> ().text = "Paused";
				else
					notifyTxt.GetComponent<Text> ().text = "Unaused";
			}
			if (deadEnemies >= deadEnemiesForEachRound [actualRound - 1]) {
				actualRound += 1;
				if (actualRound == 9)
					actualGameState = gameState.Win;
				else
					actualGameState = gameState.Shop;
			}

			if (!GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ().alive && !GameObject.FindGameObjectWithTag ("Player2").GetComponent<PlayerHealth> ().alive) {
				actualGameState = gameState.GameOver;
			}

		}
		
		if (actualGameState == gameState.GameOver) {
			helpScreen.SetActive (false);
			gui.SetActive (true);
			shop.SetActive (false);

			notifyTxt.GetComponent<Text> ().text = "GAME OVER PRESS T TO START AGAIN";
			Pauser.paused = true;
			if (Input.GetKeyDown (KeyCode.T))
				Application.LoadLevel (Application.loadedLevel);
		}

		if (actualGameState == gameState.Win) {
			helpScreen.SetActive (false);
			gui.SetActive (true);
			shop.SetActive (false);
			
			notifyTxt.GetComponent<Text> ().text = "CONGRATULATIONS YOU WIN PRESS T TO START AGAIN";
			Pauser.paused = true;
			if (Input.GetKeyDown (KeyCode.T))
				Application.LoadLevel (Application.loadedLevel);
		}

	}

	void Round1 ()
	{
		spawner2.GetComponent<Spawner> ().enemy = enemy1;
		spawner2.GetComponent<Spawner> ().countMode = true;
		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [0];
		spawner2.GetComponent<Spawner> ().spawnDelay = 3;
		
		spawner2.SetActive (true);
	}
	
	void Round2 ()
	{
		spawner1.GetComponent<Spawner> ().enemy = enemy1;
		spawner1.GetComponent<Spawner> ().countMode = true;
		spawner1.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [1] / 2;
		spawner1.GetComponent<Spawner> ().spawnDelay = 2.5f;

		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [1] / 2;
		spawner2.GetComponent<Spawner> ().spawnDelay = 2.5f;

		spawner1.SetActive (true);
		spawner2.SetActive (true);
	}

	void Round3 ()
	{

		spawner1.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [2] / 3;
		spawner1.GetComponent<Spawner> ().spawnDelay = 2.5f;

		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [2] / 3;
		spawner2.GetComponent<Spawner> ().spawnDelay = 2.5f;

		spawner3.GetComponent<Spawner> ().enemy = enemy1;
		spawner3.GetComponent<Spawner> ().countMode = true;
		spawner3.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [2] / 3;
		spawner3.GetComponent<Spawner> ().spawnDelay = 2.5f;
		
		spawner1.SetActive (true);
		spawner2.SetActive (true);
		spawner3.SetActive (true);
	}

	void BossRound ()
	{
		spawnerB.GetComponent<Spawner> ().countMode = true;
		spawnerB.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [3];
		spawnerB.GetComponent<Spawner> ().spawnDelay = 2;
		
		spawnerB.SetActive (true);
	}

	void Round5 ()
	{
		spawner2.GetComponent<Spawner> ().enemy = dinoEnemy;
		spawner2.GetComponent<Spawner> ().countMode = true;
		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [4];
		
		spawner2.SetActive (true);
	}

	void Round6 ()
	{
		spawner1.GetComponent<Spawner> ().countMode = true;
		spawner1.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [5] / 2;
		spawner1.GetComponent<Spawner> ().spawnDelay = 2.5f;

		spawner2.GetComponent<Spawner> ().enemy = dinoEnemy;
		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [5] / 2;
		spawner2.GetComponent<Spawner> ().spawnDelay = 2.5f;
		
		spawner1.SetActive (true);
		spawner2.SetActive (true);
	}

	void Round7 ()
	{
		spawner1.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [6] / 3;
		spawner1.GetComponent<Spawner> ().spawnDelay = 2.5f;
		
		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [6] / 3;
		spawner2.GetComponent<Spawner> ().spawnDelay = 2.5f;
		
		spawner3.GetComponent<Spawner> ().enemy = enemy1;
		spawner3.GetComponent<Spawner> ().countMode = true;
		spawner3.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [6] / 3;
		spawner3.GetComponent<Spawner> ().spawnDelay = 2.5f;
		
		spawner1.SetActive (true);
		spawner2.SetActive (true);
		spawner3.SetActive (true);
	}

	void BossRound2 ()
	{
		spawner1.GetComponent<Spawner> ().enemy = boss;
		spawner1.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [7] / 3;

		spawner2.GetComponent<Spawner> ().enemy = boss;
		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [7] / 3;
		
		spawner3.GetComponent<Spawner> ().enemy = boss;
		spawner3.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [7] / 3;
		
		spawner1.SetActive (true);
		spawner2.SetActive (true);
		spawner3.SetActive (true);
	}

	void ClearRound ()
	{
		spawner1.SetActive (false);
		spawner2.SetActive (false);
		spawner3.SetActive (false);
		spawnerB.SetActive (false);
		var enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (var e in enemies)
			Destroy (e);
		
		var enemies2 = GameObject.FindGameObjectsWithTag ("Enemy2");
		foreach (var e2 in enemies2)
			Destroy (e2);
		
		
		var bosses = GameObject.FindGameObjectsWithTag ("Boss");
		foreach (var b in bosses)
			Destroy (b);

		var mudTraps = GameObject.FindGameObjectsWithTag ("MudTrap");
		foreach (var t in mudTraps)
			Destroy (t);
		var spikeTraps = GameObject.FindGameObjectsWithTag ("SpikeTrap");
		foreach (var t in spikeTraps)
			Destroy (t);
	}
	
	public static T GetRandomEnum<T> ()
	{
		System.Array A = System.Enum.GetValues (typeof(T));
		T V = (T)A.GetValue (UnityEngine.Random.Range (0, A.Length));
		return V;
	}
}

