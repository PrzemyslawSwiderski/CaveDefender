using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Button exitButton;
	public Button newSingleGameButton;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		exitButton = exitButton.GetComponent<Button> ();
		newSingleGameButton = newSingleGameButton.GetComponent<Button> ();
		quitMenu.enabled = false;
	}

	public void ExitPress(){
		quitMenu.enabled = true;
		exitButton.enabled = false;
		newSingleGameButton.enabled = false;
	}
	public void NoPress(){
		quitMenu.enabled = false;
		exitButton.enabled = true;
		newSingleGameButton.enabled = true;
	}

	public void StartSingleLevel(){
		Application.LoadLevel (1);
	}


	public void ExitGame(){
		Application.Quit ();
	}

}
