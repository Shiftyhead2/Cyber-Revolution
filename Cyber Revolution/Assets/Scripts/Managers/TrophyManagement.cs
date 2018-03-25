using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyManagement : MonoBehaviour {

	public GameObject GameStatManager;
	private  TrophyManagement _instance;

	#region Kill Trophies Bools
	private bool UnlockedKillTrophy1 = false;
	private bool UnlockedKillTrophy2 = false;
	private bool UnlockedKillTrophy3 = false;
    private bool UnlockedKillTrophy4 = false;
	private  bool UnlockedKillTrophy5 = false;
	private  bool UnlockedKillTrophy6 = false;
	#endregion

	#region Survive Trophies Bools
	private bool UnlockedSurviveTrophy1 = false;
	private bool UnlockedSurviveTrophy2 = false;
	private bool UnlockedSurviveTrophy3 = false;
	private bool UnlockedSurviveTrophy4 = false;
	private bool UnlockedSurviveTrophy5 = false;
	private bool UnlockedSurviveTrophy6 = false;
	#endregion


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
		if (GameJolt.API.Manager.Instance.CurrentUser == null) {
			return;
		}

		#region Kill Trophies
		if (GameJolt.API.Manager.Instance.CurrentUser != null) {
			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.Kills == 10 && UnlockedKillTrophy1 != true) {
				GameJolt.API.Trophies.Unlock (87109, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedKillTrophy1 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.Kills == 20 && UnlockedKillTrophy2 != true) {
				GameJolt.API.Trophies.Unlock (87111, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedKillTrophy2 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.Kills == 50 && UnlockedKillTrophy3 != true) {
				GameJolt.API.Trophies.Unlock (87113, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedKillTrophy3 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.Kills == 100 && UnlockedKillTrophy4 != true) {
				GameJolt.API.Trophies.Unlock (87115, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedKillTrophy4 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.Kills == 1000 && UnlockedKillTrophy5 != true) {
				GameJolt.API.Trophies.Unlock (87117, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedKillTrophy5 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.Kills == 1000000 && UnlockedKillTrophy6 != true) {
				GameJolt.API.Trophies.Unlock (87119, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedKillTrophy6 = true;
			}

			#endregion

		#region Survive Trophies
			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.TimesSurvived == 1 && UnlockedSurviveTrophy1 != true) {
				GameJolt.API.Trophies.Unlock (87108, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedSurviveTrophy1 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.TimesSurvived == 5 && UnlockedSurviveTrophy2 != true) {
				GameJolt.API.Trophies.Unlock (87110, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedSurviveTrophy2 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.TimesSurvived == 20 && UnlockedSurviveTrophy3 != true) {
				GameJolt.API.Trophies.Unlock (87112, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedSurviveTrophy3 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.TimesSurvived == 50 && UnlockedSurviveTrophy4 != true) {
				GameJolt.API.Trophies.Unlock (87114, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedSurviveTrophy4 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.TimesSurvived == 100 && UnlockedSurviveTrophy5 != true) {
				GameJolt.API.Trophies.Unlock (87116, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedSurviveTrophy5 = true;
			}

			if (GameStatManager.GetComponent<GameStatManager> ().Gamestats.TimesSurvived == 1000 && UnlockedSurviveTrophy6 != true) {
				GameJolt.API.Trophies.Unlock (87118, (bool  success) => { 
					if (success) {
						//Debug.Log ("We got a trophy!");
					} else {
						//Debug.Log ("Something went wrong!");
					}
				});
				UnlockedSurviveTrophy6 = true;
			}
			#endregion
	}
	}
}
