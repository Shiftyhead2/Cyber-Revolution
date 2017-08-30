using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ShopManagment : MonoBehaviour {

	public GameObject MyController;
	public GameObject PlayerCanvas;
	public GameObject ShopCanvas;
	public Text MoneyText;
	public GameObject GameManager;

	void OnEnable(){
		MyController = GameObject.FindGameObjectWithTag ("Player");
		MyController.GetComponent<PlayerHP> ().PlayerIsShooping = true;
		GameManager =  GameObject.FindGameObjectWithTag ("GameManager");

	}

	void Update(){
		MoneyText.text = GameManager.GetComponent<CurrencyManager> ().Money.ToString () + "$";
		if (MyController.GetComponent<FirstPersonController> () == null) {
			//Do nothing. Don't spam me with error messages. It's pointless.
		}
		if (MyController.GetComponent<PlayerHP> ().PlayerIsShooping == true) {
			PlayerCanvas.SetActive (false);
			MyController.GetComponent<FirstPersonController> ().enabled = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			PlayerCanvas.SetActive (true);
			MyController.GetComponent<FirstPersonController> ().enabled = true;

		}
	}


	void OnDisable(){
		MyController.GetComponent<PlayerHP> ().PlayerIsShooping = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		ShopCanvas.SetActive (false);
		
	}

	public void OnExitButtonPressed(){
		OnDisable ();
		
	}
		


}
