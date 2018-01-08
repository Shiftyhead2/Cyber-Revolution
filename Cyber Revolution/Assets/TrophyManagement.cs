using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyManagement : MonoBehaviour {

	public GameObject GameStatManager;
	private  TrophyManagement _instance;
	public  bool UnlockedTrophy1 = false;


	void Awake(){
		DontDestroyOnLoad (this.gameObject);
		GameStatManager = GameObject.Find ("GameStats");
	}

	// Use this for initialization
	void Start () {
		if (!_instance) {
			_instance = this;
		} else {
			Destroy (this.gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (GameJolt.API.Manager.Instance.CurrentUser != null) {
			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.Kills == 10 && UnlockedTrophy1 != true) {
				GameJolt.API.Trophies.Unlock (87109, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedTrophy1 = true;
			}
		}
	}
}
