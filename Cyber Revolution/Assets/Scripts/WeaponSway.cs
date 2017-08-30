using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {

	#region Variables
	[Header("Weapon Sway Functions")]
	public float amount;
	public float maxAmount;
	public float smoothAmount;
	public GameObject OurParent;

	private Vector3 initialPosition;
	#endregion

	#region Start
	// Use this for initialization
	void Start () {
		initialPosition = transform.localPosition;
		OurParent = GameObject.FindGameObjectWithTag ("Player");

	}
	#endregion

	#region Update
	// Update is called once per frame
	void Update () {

		if (OurParent.GetComponent<PlayerHP> ().PlayerIsShooping == false) {
			float movementX = -Input.GetAxis ("Mouse X") * amount;
			float movementY = -Input.GetAxis ("Mouse Y") * amount;
			movementX = Mathf.Clamp (movementX, -maxAmount, maxAmount);
			movementY = Mathf.Clamp (movementY, -maxAmount, maxAmount);

			Vector3 finalPosition = new Vector3 (movementX, movementY, 0);
			transform.localPosition = Vector3.Lerp (transform.localPosition, finalPosition + initialPosition,Time.deltaTime* smoothAmount);
		}
			
		
	}
	#endregion
}
