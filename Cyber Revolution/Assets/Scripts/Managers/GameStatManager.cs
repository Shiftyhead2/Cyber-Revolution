 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameStatManager : MonoBehaviour {

	private static GameStatManager _instance;
	public GameStats Gamestats;
	public GameObject StatHolder;
	public int score;



	void Awake(){
		StatHolder = GameObject.Find ("StatHolder");
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		if (!_instance) {
			_instance = this;
		} else {
			Destroy (this.gameObject);
		}



		if (File.Exists (Application.persistentDataPath + "/gameStats.json") == true) {
			LoadStats ();
		}



		
	}

	void OnEnable(){
		Gamestats = new GameStats ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Gamestats == null) {
			Debug.LogError ("No gamestats");
		}

		score = Gamestats.Kills + Gamestats.TimesSurvived;
	}

	void OnApplicationQuit(){
		SubmitScore ();
		SaveStats ();
	}

	public void SaveStats (){
		string jsonData = JsonUtility.ToJson (Gamestats, true);
		File.WriteAllText (Application.persistentDataPath + "/gameStats.json", jsonData);
		
	}

	public  void  LoadStats(){
		Gamestats = JsonUtility.FromJson<GameStats> (File.ReadAllText(Application.persistentDataPath + "/gameStats.json"));

		if (StatHolder != null) {
			StatHolder.GetComponent<DontDestroy> ().GameStatics.Kills = Gamestats.Kills;
			StatHolder.GetComponent<DontDestroy> ().GameStatics.TimesSurvived = Gamestats.TimesSurvived;
		} 
	}

	public void SubmitScore(){
		int Score = score;
		string ScoreText = Score.ToString ();
		int tableID = 274863;
		string extradata = "";

		GameJolt.API.Scores.Add (Score, ScoreText, tableID, extradata, (bool success) => {
			Debug.Log (string.Format ("Score add [0].", success ? "Successfull" : "Failed"));
		});
			

	}

}
