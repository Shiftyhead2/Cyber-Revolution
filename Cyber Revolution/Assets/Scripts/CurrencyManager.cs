using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

	public int Money = 0;
	public Text CurrentMoneyText;

	// Use this for initialization
	void Start () {

		Money = 100;
		CurrentMoneyText.text = Money.ToString () + "$";

	}
	
	// Update is called once per frame
	void Update () {
		CurrentMoneyText.text =  Money.ToString () + "$";
		
	}
}
