using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour
{
	public static bool paused = false;
	private GameObject helpScreen;

	void Start ()
	{
	}
	// Update is called once per frame
	void Update ()
	{
		if (paused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
}
