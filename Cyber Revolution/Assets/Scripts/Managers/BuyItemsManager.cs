using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemsManager : MonoBehaviour {

	public GameObject WeaponHolder;
	public GameObject GameManager;
	public int SelectedWeapon = 0;
	public Text PriceText;
	public int Cost = 0;

	void OnEnable(){
		WeaponHolder = GameObject.FindGameObjectWithTag ("Weapon Holder");
		GameManager =  GameObject.FindGameObjectWithTag ("GameManager");
		PriceText.text = Cost.ToString () + "$";
	}

	public void OnBuy(int WhichWeapon){
		int i = 0;
		foreach (Transform weapons in WeaponHolder.transform) {
			if (i == WhichWeapon) {
				if (weapons.GetComponent<FireWeapon> ().IsActive != true && GameManager.GetComponent<CurrencyManager>().Money >= Cost ) {
					//Debug.Log ("we found a weapon that hasn't been bought yet and we have the money to buy it. Buying now!");
					GameManager.GetComponent<CurrencyManager> ().Money = GameManager.GetComponent<CurrencyManager> ().Money - Cost;
					weapons.GetComponent<FireWeapon> ().IsActive = true;
					//Debug.Log ("You bought weapon");
				} else {
					//Debug.Log ("The weapon you are trying to buy is already active or you don't have enough money.");
				}
		}
		i++;
	}
}
}
