using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoRefillManager : MonoBehaviour {

	public GameObject WeaponHolder;
	public GameObject GameManager;
	public int Cost = 500;
	public Text CostText;


    void OnEnable(){
        WeaponHolder = GameObject.FindGameObjectWithTag("Weapon Holder");
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
	}

	void Update(){
        CostText.text = Cost.ToString() + "$" + " per weapon";

    }


	public void BuyAmmo(){
		foreach (Transform weapons in WeaponHolder.transform) {
			if (weapons.GetComponent<FireWeapon> ().BulletsLeft < weapons.GetComponent<FireWeapon> ().MaxBullets && GameManager.GetComponent<CurrencyManager> ().Money >= Cost) {
				//Debug.Log ("We found weapons that have little ammo left and we have enough money to refill. Refilling");
				weapons.GetComponent<FireWeapon> ().BulletsLeft = weapons.GetComponent<FireWeapon> ().MaxBullets;
				weapons.GetComponent<FireWeapon> ().UpdateAmmo ();
				GameManager.GetComponent<CurrencyManager> ().Money = GameManager.GetComponent<CurrencyManager> ().Money - Cost;
			} else if (weapons.GetComponent<FireWeapon> ().BulletsLeft == weapons.GetComponent<FireWeapon> ().MaxBullets || GameManager.GetComponent<CurrencyManager> ().Money < Cost) {
				//Debug.Log ("You already have enough ammo for this weapon or you don't have enough money. Returning!");
			}
		}
	}



}
