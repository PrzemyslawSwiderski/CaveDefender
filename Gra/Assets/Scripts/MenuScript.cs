using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas mainMenu;
	public Canvas about;
	public Button exitButton;
	public Button aboutButton;
	public Button newSingleGameButton;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		mainMenu = mainMenu.GetComponent<Canvas> ();
		about = about.GetComponent<Canvas> ();
		exitButton = exitButton.GetComponent<Button> ();
		aboutButton = aboutButton.GetComponent<Button> ();
		newSingleGameButton = newSingleGameButton.GetComponent<Button> ();
		quitMenu.enabled = false;
		about.enabled = false;
		mainMenu.enabled = true;
	}

	public void ExitPress(){
		mainMenu.enabled = false;
		quitMenu.enabled = true;
		exitButton.enabled = false;
		newSingleGameButton.enabled = false;
	}
	public void NoPress(){
		mainMenu.enabled = true;
		quitMenu.enabled = false;
		exitButton.enabled = true;
		newSingleGameButton.enabled = true;
		about.enabled = false;
	}
	public void AboutPress(){
		mainMenu.enabled = false;
		quitMenu.enabled = false;
		exitButton.enabled = false;
		newSingleGameButton.enabled = false;
		about.enabled = true;
	}

	public void StartSingleLevel(){
		Application.LoadLevel (1);
	}


	public void ExitGame(){
		Application.Quit ();
	}

}
