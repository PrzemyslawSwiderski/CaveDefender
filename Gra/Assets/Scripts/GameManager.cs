using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum gameState
{
	Tutorial,
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
	public int buildStonesToBuildTower;
	public bool towerBuilded = false;
	public int actualRound = 1;
	public string notifyToShow;
	public float tutorialTime;

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
	public int[] deadEnemiesForEachRound = {5,10,20,1,15,20,30,30,3};


	// Use this for initialization
	void Start ()
	{
		instance = this;  	//Singleton pattern, always first

		if (actualGameState == gameState.Tutorial) {
			Invoke ("displayHelp1", 1);
		
			Invoke ("displayHelp2", 6);
		
			Invoke ("displayHelp3", 11);
		
			Invoke ("displayHelp4", 16);
			
			Invoke ("displayHelp5", 21);
			
			Invoke ("displayHelp6", 26);
			
			Invoke ("displayHelp7", 31);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		notifyService ();
		if (Input.GetKey (KeyCode.Escape))
			Application.LoadLevel (0);

		if (actualGameState == gameState.Tutorial) {
			tutorialTime -= Time.deltaTime;

			helpScreen.SetActive (false);
			gui.SetActive (true);
			shop.SetActive (false);
			if (Input.GetKeyUp (KeyCode.Return) && tutorialTime <= 0.0f) {
				actualGameState = gameState.RoundStart;
			}
		}


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
			enterInfo.GetComponent<Text> ().text = "Press Space To Start Round Number: " + actualRound.ToString ();

			if (Input.GetKeyUp (KeyCode.Space)) {
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
				if (actualRound == 4 || actualRound == 8 || actualRound == 9)
					notifyTxt.GetComponent<Text> ().text = "TIME TO START ROUND WITH BOSS!!! " + " : " + Mathf.Abs (Mathf.Round (timeToStart)).ToString () + " s";
				else
					notifyTxt.GetComponent<Text> ().text = "TIME TO START ROUND " + actualRound.ToString () + " : " + Mathf.Abs (Mathf.Round (timeToStart)).ToString () + " s";
			
			} else {
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
				case 9:
					BossRound3 ();
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
				if (actualRound == 10)
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
		spawner1.GetComponent<Spawner> ().enemy = dinoEnemy;
		spawner1.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [7] / 3;

		spawner2.GetComponent<Spawner> ().enemy = boss;
		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [7] / 3;
		
		spawner3.GetComponent<Spawner> ().enemy = enemy1;
		spawner3.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [7] / 3;
		
		spawner1.SetActive (true);
		spawner2.SetActive (true);
		spawner3.SetActive (true);
	}
		
	void BossRound3 ()
	{
		spawner1.GetComponent<Spawner> ().enemy = boss;
		spawner1.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [8] / 3;

		spawner2.GetComponent<Spawner> ().enemy = boss;
		spawner2.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [8] / 3;
		
		spawner3.GetComponent<Spawner> ().enemy = boss;
		spawner3.GetComponent<Spawner> ().howManyToSpawn = deadEnemiesForEachRound [8] / 3;
		
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
		var standingSpots = GameObject.FindGameObjectsWithTag ("SpikeTrap");
		foreach (var t in spikeTraps)
			Destroy (t);
	}
	
	public static T GetRandomEnum<T> ()
	{
		System.Array A = System.Enum.GetValues (typeof(T));
		T V = (T)A.GetValue (UnityEngine.Random.Range (0, A.Length));
		return V;
	}

	void notifyService ()
	{
		notifyTxt.GetComponent<Text> ().text = notifyToShow;
	}

	public void displayNotify (string message)
	{
		notifyToShow = message;
		Invoke ("hideNotify", 2.0f);
	}

	void displayHelp1 ()
	{
		notifyToShow = "Collect bones to upgrade weapons after each round.";
		Invoke ("hideNotify", 4.0f);
	}

	void displayHelp2 ()
	{
		notifyToShow = "To build a tower you have to collect "+buildStonesToBuildTower+" stones.";
		Invoke ("hideNotify", 4.0f);
	}

	void displayHelp3 ()
	{
		notifyToShow = "Tower's arrows will be hurting enemies only.";
		Invoke ("hideNotify", 4.0f);
	}
	void displayHelp4 ()
	{
		notifyToShow = "Stand on ellipses with both players to toggle superpower.";
		Invoke ("hideNotify", 4.0f);
	}

	void displayHelp5 ()
	{
		notifyToShow = "Change weapon to one of 3 traps, and hold fire to plant.";
		Invoke ("hideNotify", 4.0f);
	}

	void displayHelp6 ()
	{
		notifyToShow = "Try to avoid arrows and not to hurt second player.";
		Invoke ("hideNotify", 4.0f);
	}

	void displayHelp7 ()
	{
		notifyToShow = "Press enter to continue. Good luck !!!";
		Invoke ("hideNotify", 4.0f);
	}

	void hideNotify ()
	{
		notifyToShow = "";
	}

	void exitPause ()
	{
		Pauser.paused = false;
	}

}

