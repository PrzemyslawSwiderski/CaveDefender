using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime;		// The amount of time between each spawn.
	private float translate;
	public GameObject enemy;
	public GameObject warning;
	public bool countMode;
	public int howManyToSpawn;
	public float spawnDelay = 1f;      // The amount of time before spawning starts.

	void Start ()
	{
		spawnTime = 5f / GameObject.Find ("GameManager").GetComponent<GameManager> ().actualRound;
		// Start calling the Spawn function repeatedly after a delay .
		if(!countMode)
		InvokeRepeating ("Spawn", spawnDelay, spawnTime);
	}

	void OnEnable ()
	{
		if (countMode) {
			for (int i=0; i<howManyToSpawn; i++)
				Invoke ("Spawn", spawnDelay * i);
		} 
	}

	void Spawn ()
	{
		if (enemy.name == "arrow 1") {
			translate = transform.position.x + Random.Range (0.1f, 15.0f);
			transform.Translate (translate, 0, 0);
			transform.Translate (0, 5, 0);
			Instantiate (warning, transform.position, transform.rotation);
			transform.Translate (0, -5, 0);
			Invoke ("spawnArrow", 1.0f);
			transform.Translate (-translate, 0, 0);
		} else {
			Instantiate (warning, transform.position, transform.rotation);
			Instantiate (enemy, transform.position, transform.rotation);
		}
	}

	void spawnArrow ()
	{
		transform.Translate (translate, 0, 0);
		Instantiate (enemy, transform.position, transform.rotation);
		transform.Translate (-translate, 0, 0);
	}
}
