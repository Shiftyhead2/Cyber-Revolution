using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCanvasManager : MonoBehaviour {

	public Button ResumeGame;
	public Button ExitGame;
	public GameObject GameManager;


	void OnEnable(){
		ResumeGame.onClick.AddListener (delegate {Resume();});
		ExitGame.onClick.AddListener (delegate {Exit ();});
		GameManager = GameObject.FindGameObjectWithTag ("GameManager");

	}

	public void Resume(){
		GameManager.GetComponent<PauseManager> ().IsPaused = false;
	}

	public void Exit(){
		Application.Quit();
	}
	


}
