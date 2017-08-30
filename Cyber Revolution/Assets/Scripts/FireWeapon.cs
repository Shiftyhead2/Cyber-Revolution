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
	public ParticleSystem muzzleFlash; //muzzle flash particle system
	public GameObject HitParticles; //hit particles that spawn
	public GameObject BulletObject; //Bullet holes
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

	[Header("UI")]
	public Text CurrentAmmoText;
	public Text BulletsLeftAmmoText;
	public GameObject Crosshair;
	[SerializeField]private Animator CrosshairAnimator;

	#endregion

	#region Start
	// Use this for initialization
	void Start () {
		Crosshair = GameObject.FindGameObjectWithTag ("Crosshair");
		MyAudioSource = GetComponent<AudioSource> ();
		OurParent = GameObject.FindGameObjectWithTag ("Player");
		GameManager = GameObject.FindGameObjectWithTag ("GameManager");
		CurrentBullets = BulletsPerMag;
		CurrentAmmoText.text = CurrentBullets.ToString ();
		BulletsLeftAmmoText.text = BulletsLeft.ToString ();
		CrosshairAnimator = Crosshair.GetComponent<Animator> ();
		maximumSpread = maxSpread;
	}
	#endregion

	#region Update
	// Update is called once per frame
	void Update () {
		//This is the input where if you press mouse 1 or ctrl you shoot if you have bullets however if you don't you automaticly reload
		if (Input.GetButton ("Fire1")) {
			if (CurrentBullets > 0) {
				Fire ();
			} else if(BulletsLeft > 0) {
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
			if (Crosshair != null) {
				CrosshairAnimator.CrossFadeInFixedTime ("Aiming", 0.1f);
			}
		} else {
			SteadyAim = false;
			maxSpread = maximumSpread;
			if (Crosshair != null) {
				CrosshairAnimator.CrossFadeInFixedTime ("Normal", 0.1f);
			}
		}


		//Firerate checking
		if (firetimer < firerate) {
			firetimer += Time.deltaTime;
		}
		CurrentAmmoText.text = CurrentBullets.ToString ();
		BulletsLeftAmmoText.text = BulletsLeft.ToString ();

		if (SteadyAim != false) {
			StableAim ();
		}

		//Checking if the game is paused
		if (GameManager.GetComponent<PauseManager> ().IsPaused == true) {
			OurParent.GetComponent<FirstPersonController> ().enabled = false;
			OurParent.GetComponentInChildren<WeaponSwitching>().enabled = false;
			OurParent.GetComponent<PlayerHP> ().enabled = false;
		} else {
			OurParent.GetComponent<FirstPersonController> ().enabled = true;
			OurParent.GetComponentInChildren<WeaponSwitching>().enabled = true;
			OurParent.GetComponent<PlayerHP> ().enabled = true;
		}



	}
	#endregion 

	#region FixedUpdate
	void FixedUpdate(){
		//Getting the animator info then using it for reload animation
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);
		
		IsReload = info.IsName ("Reload");

		}
	#endregion





	#region Firing the weapon
	private void Fire(){
		//Checking if all of these are not true. 
		if (firetimer < firerate || CurrentBullets <= 0 || IsReload || OurParent.GetComponent<PlayerHP>().PlayerIsShooping != false || GameManager.GetComponent<PauseManager>().IsPaused) 
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
		muzzleFlash.Play ();
		PlayAudioClip ();


		CurrentBullets--;
		CurrentAmmoText.text = CurrentBullets.ToString ();
		firetimer = 0.0f;

	}
	#endregion

	#region Reload
	public void Reload(){
		if (BulletsLeft <= 0)
			return;
		
		int BulletsToLoad = BulletsPerMag - CurrentBullets;
		int BulletsToDeduct = (BulletsLeft >= BulletsToLoad) ? BulletsToLoad : BulletsLeft;

		BulletsLeft -= BulletsToDeduct;
		CurrentBullets += BulletsToDeduct;
		CurrentAmmoText.text = CurrentBullets.ToString ();
		BulletsLeftAmmoText.text = BulletsLeft.ToString ();



	}
	#endregion

	#region DoReload
	private void DoReload(){
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);

		if (IsReload)
			return;
		
		anim.CrossFadeInFixedTime ("Reload", 0.01f);
		MyAudioSource.PlayOneShot (ClipOut);
	}
	#endregion

	private void PlayAudioClip(){
		MyAudioSource.PlayOneShot (ShootSound);
	}

	private void StableAim(){
		maxSpread = aimSpread;

	}


}
