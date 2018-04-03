using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class FireWeapon : MonoBehaviour {

	#region Variables
	[Header("Animator")]
	public Animator anim;



	[Header("Shop config")]
	public bool IsActive;


	[Header("Weapon Statics")]
	public bool HasHalfReload; //Checks if the gun in question has an alternative reload animation
	public float range = 100f; // Range of the weapon
	public int BulletsPerMag = 30; //Bullets Per Each Magazine
	public int BulletsLeft = 200; // Bullets Left
	public int CurrentBullets; // Current Bullets in Magazine
	public int MaxBullets; //The maximum number of bullets(used for store only)
	public float damage = 20f; // Damage the weapon does
	public float ArmorPenetration;//Armor penetration
	public float normalSpread = 0.0f; // Spread before firing
	public float TimeTillNextSpread = 0.01f; //Time till next spread
	public float maxSpread = 0.04f; // Maximum spread
	[SerializeField]private float maximumSpread;//Maximum spread a gun can have
	public float aimSpread;//Spread when "aiming"

	[Header("ShotgunOnly")]
	public int ShotgunPellets; //How many pellets does a shotgun fire?


	[Header("Weapon Config")]
	public Transform ShootPoint; //the point where the raycast is spawned
	public Transform Shell; //shell object which the gun ejects
	public Transform ShellEjection; //shell object from which the shell is ejected
	public bool CanEject;//bool to check if the said gun can eject shells
	public ParticleSystem muzzleFlash; //muzzle flash particle system
	public GameObject HitParticles; //hit particles that spawn
	public GameObject BulletObject; //Bullet holes
    public Camera OurCamera;
    public int AimFov;
    public int NormalFov = 60;
	public bool SteadyAim = false;//Checks if we are currently aiming

	[Header("Audio")]
	AudioSource MyAudioSource;
	public AudioClip ShootSound;//Sound for shooting
	public AudioClip ClipOut;//Sound when starting to reload



	public GameObject OurParent;
	private GameObject GameManager;

	[Header("Weapon Firerate")]
	public float firerate = 0.1f; //fire rate of the weapon

	float firetimer;

	private bool IsReload;
	private bool IsHalfReload;

	[Header("UI")]
	public GameObject AmmoUI;
	public Text AmmoUIText;

	#endregion

	void OnEnable(){
        AmmoUI = GameObject.Find ("Ammo Text");
		AmmoUIText = AmmoUI.GetComponent<Text> ();
        UpdateAmmo ();
	}

	#region Start
	// Use this for initialization
	void Start () {
		OurParent = GameObject.FindGameObjectWithTag ("Player");
		GameManager = GameObject.FindGameObjectWithTag ("GameManager");
		AmmoUI = GameObject.Find ("Ammo Text");
		AmmoUIText = AmmoUI.GetComponent<Text> ();
		MyAudioSource = GetComponent<AudioSource> ();
        BulletsLeft = MaxBullets;
		CurrentBullets = BulletsPerMag;
		maximumSpread = maxSpread;
		UpdateAmmo ();
	}
	#endregion

	#region Update
	// Update is called once per frame
	void Update ()
	{
        

        if (GameManager.GetComponent<PauseManager> ().IsPaused != true  ){
			//Debug.Log ("Enabling all player fuctions!");
			OurParent.GetComponent<FirstPersonController> ().enabled = true;
			OurParent.GetComponentInChildren<WeaponSwitching>().enabled = true;
			OurParent.GetComponent<PlayerHP> ().enabled = true;
			OurParent.GetComponentInChildren<WeaponSway> ().enabled = true;

		}

		//Checking if the game is paused or if the game has been won
		if (GameManager.GetComponent<PauseManager> ().IsPaused == true || GameManager.GetComponent<WaveSpawner>().GameWon == true  ) {
			//Debug.Log ("Disabling all player fuctions");
			OurParent.GetComponent<FirstPersonController> ().enabled = false;
			OurParent.GetComponentInChildren<WeaponSwitching>().enabled = false;
			OurParent.GetComponent<PlayerHP> ().enabled = false;
			OurParent.GetComponentInChildren<WeaponSway> ().enabled = false;
		}  


		//This is the input where if you press mouse 1 or ctrl you shoot if you have bullets
		if(Input.GetButton("Fire1") &&  GameManager.GetComponent<WaveSpawner>().GameWon != true ){
			if (CurrentBullets > 0) {
				Fire ();
			} else if (BulletsLeft > 0) {
				DoReload ();
			}
		}
			
		

		//Manual reloading 
		if (Input.GetKeyDown (KeyCode.R)) {
			if (CurrentBullets < BulletsPerMag && BulletsLeft > 0) {
				DoReload ();
			}
		}

		//Steady aim
		if (Input.GetButton ("Fire2")) {
			SteadyAim = true;
            OurCamera.fieldOfView = AimFov;
		} else {
			SteadyAim = false;
			maxSpread = maximumSpread;
            OurCamera.fieldOfView = NormalFov;
        }
	



		//Firerate checking
		if (firetimer < firerate) {
			firetimer += Time.deltaTime;
		}


		if (SteadyAim != false) {
			StableAim ();
		}


	}

	#endregion 

	#region FixedUpdate
	void FixedUpdate(){
		//Getting the animator info then using it for reload animation
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);
		
		IsReload = info.IsName ("Reload");
		IsHalfReload = info.IsName ("HalfReload");

		}
	#endregion





	#region Firing the weapon
	private void Fire(){
		//Checking if all of these are not true. 
		if (firetimer < firerate || CurrentBullets <= 0 || IsReload ||IsHalfReload|| OurParent.GetComponent<PlayerHP>().PlayerIsShooping != false || GameManager.GetComponent<PauseManager>().IsPaused) 
			return;
		
		RaycastHit hit;
		// Bullet Spread
		if (GameObject.FindWithTag("Shotgun") == true) {
			for (int i = 0; i < ShotgunPellets; i++) {
				Vector3 direction = ShootPoint.forward; 
				float shotgunSpread = Mathf.Lerp (normalSpread, maxSpread, firerate / TimeTillNextSpread );
				direction.x += Random.Range (-shotgunSpread, shotgunSpread);
				direction.y += Random.Range (-shotgunSpread, shotgunSpread);
				direction.z += Random.Range (-shotgunSpread, shotgunSpread);


				if (Physics.Raycast (ShootPoint.position, direction, out hit, range)) {
					//Debug.Log (hit.transform.name + "found");


					GameObject hitParticlesEffect = Instantiate (HitParticles, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal));
					hitParticlesEffect.transform.SetParent (hit.transform); 
					//Spawning both hit particles and bullet holes and setting their parents to whatever the ray cast hit
					GameObject BulletObjectEffect = Instantiate (BulletObject, hit.point, Quaternion.FromToRotation (Vector3.forward, hit.normal));
					BulletObjectEffect.transform.SetParent (hit.transform);




					Destroy (BulletObjectEffect, 5f);
					Destroy (hitParticlesEffect, 1f);

					if (hit.transform.GetComponent<EnemyHealth> ()) {
						hit.transform.GetComponent<EnemyHealth> ().ApplyDamage (damage + ArmorPenetration);
					}
				}
			}
		} else {
			Vector3 direction = ShootPoint.forward; 
			float currentSpread = Mathf.Lerp (normalSpread, maxSpread, firerate / TimeTillNextSpread );
			direction.x += Random.Range (-currentSpread, currentSpread);
			direction.y += Random.Range (-currentSpread, currentSpread);
			direction.z += Random.Range (-currentSpread, currentSpread);
			if (Physics.Raycast (ShootPoint.position, direction, out hit, range)) {
				//Debug.Log (hit.transform.name + "found");


				GameObject hitParticlesEffect = Instantiate (HitParticles, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal));
				hitParticlesEffect.transform.SetParent (hit.transform); 
				//Spawning both hit particles and bullet holes and setting their parents to whatever the ray cast hit
				GameObject BulletObjectEffect = Instantiate (BulletObject, hit.point, Quaternion.FromToRotation (Vector3.forward, hit.normal));
				BulletObjectEffect.transform.SetParent (hit.transform);




				Destroy (BulletObjectEffect, 5f);
				Destroy (hitParticlesEffect, 1f);

				if (hit.transform.GetComponent<EnemyHealth> ()) {
					hit.transform.GetComponent<EnemyHealth> ().ApplyDamage (damage + ArmorPenetration);
				}
			}
		}


		anim.CrossFadeInFixedTime ("Fire", 0.01f);
		StartCoroutine (PlayGunEffect ());
		PlayAudioClip ();

		if (CanEject) {
			SpawnShells ();
		}

		CurrentBullets--;
		UpdateAmmo ();
		firetimer = 0.0f;

	}
	#endregion

	IEnumerator PlayGunEffect(){
		muzzleFlash.Play ();
		yield return new WaitForEndOfFrame ();
	}

	#region Reload
	public void Reload(){
		if (BulletsLeft <= 0)
			return;
		
		int BulletsToLoad = BulletsPerMag - CurrentBullets;
		int BulletsToDeduct = (BulletsLeft >= BulletsToLoad) ? BulletsToLoad : BulletsLeft;

		BulletsLeft -= BulletsToDeduct;
		CurrentBullets += BulletsToDeduct;
		UpdateAmmo ();



	}
	#endregion

	#region DoReload
	private void DoReload(){
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);

		if (IsReload || IsHalfReload)
			return;
		
		if (CurrentBullets < BulletsPerMag && CurrentBullets > 0 && HasHalfReload == true ) {
			anim.CrossFadeInFixedTime ("HalfReload", 0.01f);
		} else {
			anim.CrossFadeInFixedTime ("Reload", 0.01f);
		}

		MyAudioSource.PlayOneShot (ClipOut);

	}
	#endregion

	private void PlayAudioClip(){
		MyAudioSource.PlayOneShot (ShootSound);
	}

	private void StableAim(){
		maxSpread = aimSpread;
}
	public void UpdateAmmo(){
		AmmoUIText.text = CurrentBullets.ToString () + "/" + BulletsLeft.ToString ();
	}

	public void SpawnShells(){
		Instantiate (Shell, ShellEjection.position,ShellEjection.rotation);
	}

}





