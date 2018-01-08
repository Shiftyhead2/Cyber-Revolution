using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	private DontDestroy _instance;
	public  GameStats GameStatics;

	// Use this for initialization
	void Start () {
		if (!_instance) {
			_instance = this;
		} else {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);

	}

	void Update(){
		if (GameStatics == null) {
			Debug.LogError ("No gamestatics");
		}
	}

	void OnEnable(){
		GameStatics = new GameStats ();
	}
	

}
