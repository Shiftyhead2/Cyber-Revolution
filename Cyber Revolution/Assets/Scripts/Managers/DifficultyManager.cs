using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {

	private DifficultyManager _instance;


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
		
	}
}
