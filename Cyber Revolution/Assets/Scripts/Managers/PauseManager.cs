﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	public bool IsPaused;
	public GameObject PauseCanvas;
	public GameObject PlayerCanvas;
	public GameObject Player;
	public GameObject GameManager;
	public Canvas PauseMenuCanvas;

	// Use this for initialization
	void Start () {
		PauseCanvas = GameObject.Find ("Pause Canvas");
		PlayerCanvas = GameObject.Find ("PlayerCanvas 1");
		GameManager = GameObject.Find ("GameManager");
		Player = GameObject.FindGameObjectWithTag ("Player");
		IsPaused = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && Player != null && Player.GetComponent<PlayerHP>().PlayerIsShooping != true && GameManager.GetComponent<WaveSpawner>().GameWon !=true) {
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
		if (Player != null) {
			if (Player.GetComponent<PlayerHP> ().PlayerIsShooping != true && GameManager.GetComponent<WaveSpawner>().GameWon !=true ) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
		Time.timeScale = 1f;
	}
}
