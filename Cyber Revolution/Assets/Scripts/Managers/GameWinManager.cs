using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWinManager : MonoBehaviour {

	public Button Restart;
	public Button MainMenu;
	public Button Exit;
	public Canvas GameWinCanvas;
	public Text KillsText;
	public GameObject  GameStatsManager;

	void Awake(){
		GameStatsManager = GameObject.Find ("GameStats");
		if (GameStatsManager == null) {
			//Do nothing
		}
	}

	void OnEnable(){
		Restart.onClick.AddListener (delegate {RestartLevel ();});
		MainMenu.onClick.AddListener (delegate {BackToMenu ();});
		Exit.onClick.AddListener (delegate {ExitApplication ();});
		if (GameStatsManager != null) {
			KillsText.text = "Kills: " + GameStatsManager.GetComponent<GameStatManager> ().Gamestats.Kills.ToString ();
		} else {
			KillsText.text = "Kills:0";
		}

		if (GameWinCanvas.enabled != false) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}




	public void RestartLevel(){
		//Debug.Log ("Restarting level");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


	}
		
	public void BackToMenu(){
		//Debug.Log ("Going back to menu");
		SceneManager.LoadScene (0);
	}

	public void ExitApplication(){
		//Debug.Log ("Exiting game");
		Application.Quit ();

	}
}
