using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

	public GameObject MainMenuCanvas;
	public GameObject SettingsCanvas;
	public Button PlayButton;
	public Button SettingsButton;
	public Button ExitButton;

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
		MainMenuCanvas.GetComponentInChildren<Canvas> ().enabled = false;
		SettingsCanvas.GetComponentInChildren<Canvas> ().enabled = true;
		
	}

	public void OnExitButtonClick(){
		Application.Quit ();
	}



}
