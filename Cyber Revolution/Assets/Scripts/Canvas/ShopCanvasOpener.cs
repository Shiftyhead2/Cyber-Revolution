using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvasOpener : MonoBehaviour {

	public GameObject ShopInfo;
	public GameObject ShopCanvas;
	public GameObject PlayerCanvas;
	public bool CanShop = false;


	void Update(){
		if (Input.GetKeyDown (KeyCode.B) && CanShop == true ) {
			ShopCanvas.SetActive (true);
			PlayerCanvas.SetActive (false);
		
		}
	}




	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			ShopInfo.SetActive (true);
			CanShop = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			ShopInfo.SetActive (false);
			CanShop = false;
		}
	}



}
