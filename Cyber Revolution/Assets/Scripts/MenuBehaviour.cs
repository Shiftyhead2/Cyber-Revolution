using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

	public bool IsSigned = false;
	public bool IsInSettings = false;


	public GameObject MainMenuCanvas;
	public GameObject SettingsCanvas;
	public GameObject GameStatsManager;
	public Button PlayButton;
	public Button SettingsButton;
	public Button ExitButton;

	void Start(){
		if (GameJolt.API.Manager.Instance.CurrentUser == null  && IsSigned ==  false) {
			GameJolt.UI.Manager.Instance.ShowSignIn ((bool success) => {
				if (success) {
					//Debug.Log ("Succesfully Logged in!Welcome user: " + GameJolt.API.Manager.Instance.CurrentUser.Name);
				} else { 
					//Debug.Log ("The user did not succesfully log in!");
					GameJolt.UI.Manager.Instance.ShowSignIn();
				}
			});
		}
		GameStatsManager = GameObject.Find ("GameStats");
	}

	void Update(){
		if (GameJolt.API.Manager.Instance.CurrentUser != null) {
			IsSigned = true;
		} else {
			IsSigned = false;
		}

		if (IsSigned == false || IsInSettings!=false) {
			MainMenuCanvas.GetComponentInChildren<Canvas> ().enabled = false;
		} else if (IsSigned != false && IsInSettings == false) {
			MainMenuCanvas.GetComponentInChildren<Canvas> ().enabled = true;
		}

	}

	void OnEnable(){
		if (SettingsCanvas.GetComponentInChildren<Canvas> ().enabled == true) {
			SettingsCanvas.GetComponentInChildren<Canvas> ().enabled = false;
		} else {
			//Do nothing.The object in question is already disabled
		}

	
		SettingsButton.onClick.AddListener (delegate {OnSettingsButtonClick ();});
		ExitButton.onClick.AddListener (delegate {OnExitButtonClick ();});

	}



	public void OnSettingsButtonClick(){
		IsInSettings = true;
		SettingsCanvas.GetComponentInChildren<Canvas> ().enabled = true;
		
	}

	public void OnExitButtonClick(){
		GameStatsManager.GetComponent<GameStatManager> ().SaveStats ();
		Application.Quit ();
	}



}
