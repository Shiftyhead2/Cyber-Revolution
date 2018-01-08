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

	void Start(){
		GameStatsManager = GameObject.Find ("GameStats");
	}

	void OnEnable(){
		Restart.onClick.AddListener (delegate {RestartLevel ();});
		MainMenu.onClick.AddListener (delegate {BackToMenu ();});
		Exit.onClick.AddListener (delegate {ExitApplication ();});
	}

	void Update(){
		if (GameWinCanvas.enabled != false) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			KillsText.text = "Kills: " + GameStatsManager.GetComponent<GameStatManager> ().Gamestats.Kills.ToString ();
		}
	}


	public void RestartLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


	}

	public void BackToMenu(){
		SceneManager.LoadScene (0);
	}

	public void ExitApplication(){
		Application.Quit ();

	}
}
