using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoRefillManager : MonoBehaviour {

	public GameObject WeaponHolder;
	public GameObject GameManager;
	public int Cost = 500;
	public Text CostText;
	public int CostMultiplier;

	void OnEnable(){
		WeaponHolder = GameObject.FindGameObjectWithTag ("Weapon Holder");
		GameManager =  GameObject.FindGameObjectWithTag ("GameManager");
		CostText.text = Cost.ToString () + "$";
		CheckAmmo ();
	}

	void Update(){
		CostText.text = Cost.ToString () + "$";

	}

	private void CheckAmmo(){
		foreach (Transform weapons in WeaponHolder.transform) {
			if (weapons.GetComponent<FireWeapon> ().IsActive == true) {
				if (weapons.GetComponent<FireWeapon> ().BulletsLeft < weapons.GetComponent<FireWeapon> ().MaxBullets && CostMultiplier <= WeaponHolder.transform.childCount -1) {
					CostMultiplier++;
					Cost = Cost + (CostMultiplier * 2);
				} else if (weapons.GetComponent<FireWeapon> ().BulletsLeft >= weapons.GetComponent<FireWeapon> ().MaxBullets || CostMultiplier == 0) {
					Cost = 100;
				}
			} else {
				//Do nothing

			}

		}
	}

	public void BuyAmmo(){
		foreach (Transform weapons in WeaponHolder.transform) {
			if (weapons.GetComponent<FireWeapon> ().BulletsLeft < weapons.GetComponent<FireWeapon> ().MaxBullets && GameManager.GetComponent<CurrencyManager> ().Money >= Cost) {
				//Debug.Log ("We found weapons that have little ammo left and we have enough money to refill. Refilling");
				weapons.GetComponent<FireWeapon> ().BulletsLeft = weapons.GetComponent<FireWeapon> ().MaxBullets;
				weapons.GetComponent<FireWeapon> ().UpdateAmmo ();
				GameManager.GetComponent<CurrencyManager> ().Money = GameManager.GetComponent<CurrencyManager> ().Money - Cost;
				CostMultiplier = 0;
				CheckAmmo ();
			} else if (weapons.GetComponent<FireWeapon> ().BulletsLeft >= weapons.GetComponent<FireWeapon> ().MaxBullets || GameManager.GetComponent<CurrencyManager> ().Money < Cost) {
				//Debug.Log ("You already have enough ammo for this weapon or you don't have enough money. Returning!");
			}
		}
	}



}
