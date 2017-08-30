using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	public bool IsPaused;
	public GameObject PauseCanvas;
	public GameObject PlayerCanvas;
	public GameObject Player;
	public Canvas PauseMenuCanvas;

	// Use this for initialization
	void Start () {
		PauseCanvas = GameObject.Find ("Pause Canvas");
		PlayerCanvas = GameObject.Find ("PlayerCanvas");
		Player = GameObject.FindGameObjectWithTag ("Player");
		IsPaused = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && Player != null && Player.GetComponent<PlayerHP>().PlayerIsShooping != true) {
			IsPaused = !IsPaused;
		}

		if (Player == null) {
			//Don't spam me with fucking stupid error messages. The player is dead.
		}

		if (IsPaused) {
			PauseGame ();
		} else if (!IsPaused) {
			UnpauseGame ();
		}
	}

	public void PauseGame(){
		PauseMenuCanvas.enabled = true;
		PlayerCanvas.SetActive (false);
		if (IsPaused == true) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		Time.timeScale = 0f;
	}

	public void UnpauseGame(){
		PauseMenuCanvas.enabled = false;
		PlayerCanvas.SetActive (true);
		if (Player.GetComponent<PlayerHP> ().PlayerIsShooping != true) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		Time.timeScale = 1f;
	}
}
