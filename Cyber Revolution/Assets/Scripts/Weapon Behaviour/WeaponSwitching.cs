using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

	public int SelectedWeapon = 0;
	public GameObject OurParent;
	public int previousSelectedWeapon;


	// Use this for initialization
	void Start () {
		SelectWeapon ();
		OurParent = GameObject.FindGameObjectWithTag ("Player");
		previousSelectedWeapon = SelectedWeapon;
		
	}
	
	// Update is called once per frame
	void Update () {

		previousSelectedWeapon = SelectedWeapon;


		if (OurParent.GetComponent<PlayerHP> ().PlayerIsShooping == false) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0f) {
				if (SelectedWeapon >= transform.childCount - 1) {
					SelectedWeapon = 0;
				} else {
					SelectedWeapon++;
				}
			}

			if (Input.GetAxis ("Mouse ScrollWheel") < 0f) {
				if (SelectedWeapon <= 0) {
					SelectedWeapon = transform.childCount - 1;
				} else {
					SelectedWeapon--;
				}
			}

		}


		if (previousSelectedWeapon != SelectedWeapon) {
				SelectWeapon ();
		}

 	}

		
	



	void SelectWeapon()
	{
		int i = 0;
		bool hasChanged = false;
		foreach (Transform weapon in transform)
		{
			if (i == SelectedWeapon && weapon.GetComponent<FireWeapon> ().IsActive == true)
			{
				
					hasChanged = true;
                    weapon.gameObject.SetActive(true);
			}
			i++;
		}

		i = 0;

		foreach (Transform weapon in transform)
		{
			if (i != SelectedWeapon &&  hasChanged ==  true)
			{
				weapon.gameObject.SetActive (false);
			}
			i++;
		}
	}

}