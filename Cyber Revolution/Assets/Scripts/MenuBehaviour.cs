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
	public Button PlayButton;
	public Button SettingsButton;
	public Button ExitButton;

	void Start(){
		GameJolt.UI.Manager.Instance.ShowSignIn ();
	}

	void Update(){
		if (GameJolt.API.Manager.Instance.CurrentUser == null) {
			IsSigned = false;
		} else {
			IsSigned = true;
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
		Application.Quit ();
	}



}
