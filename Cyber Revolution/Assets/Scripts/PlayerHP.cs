using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

	#region Variables
	[Header("Player Health")]
	[SerializeField] private float PlayerHealthPoints = 100f;
	public Text PlayerHPtext;
	public Image DamageScreen;
	public Image HealingScreen;
	public Slider HealthSlider;
	Color flashcolor = new Color (255f, 255f, 255f, 1f);
	public float FlashSpeed = 5f;
	bool Damaged = false;
	public GameObject MainCamera;
	public Canvas PlayerCanvas;
	public Canvas GameOverCanvas;
	public bool PlayerIsShooping = false;
	public float HealingTime;
	public float HealingRate = 2f;
	public float HealingAmountTime = 5f;
	private float HealingAmount = 10f;
	public bool IsHealing = false;

	AudioSource MyAudioSource;
	public AudioClip HurtSoundClip;

	#endregion

	#region Start
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		PlayerHPtext.text = PlayerHealthPoints.ToString ();
		MainCamera.GetComponent<Camera> ().enabled = false;
		MainCamera.GetComponent<AudioListener> ().enabled = false;
		PlayerCanvas.enabled = true;
		GameOverCanvas.enabled = false;
		MyAudioSource = GetComponent<AudioSource> ();
		HealingTime = HealingRate;
		HealthSlider.value = HealingTime;
		HealthSlider.maxValue = HealingRate;
		
	}
	#endregion


	#region Update
	void Update(){
		if (Damaged) {
			DamageScreen.color = flashcolor;
		} else {
			DamageScreen.color = Color.Lerp (DamageScreen.color, Color.clear, FlashSpeed*Time.deltaTime);
		}
		if (IsHealing != true) {
			HealingScreen.color = Color.Lerp (HealingScreen.color, Color.clear, FlashSpeed*Time.deltaTime);
		}

		Damaged = false;

		if (Input.GetKeyDown (KeyCode.Q)) {
			Heal ();
		}

		if (HealingTime < HealingRate) {
			HealingTime += Time.deltaTime;
		}

		HealthSlider.value = HealingTime;


	}
	#endregion


	#region ApplyPlayerDamage
	public void ApplyPlayerDamage(float damage){
		PlayerHealthPoints -= damage;
		Damaged = true;
		MyAudioSource.PlayOneShot (HurtSoundClip);
		if (PlayerHealthPoints <= 0f) {
			MainCamera.GetComponent<Camera> ().enabled = true;
			MainCamera.GetComponent<AudioListener> ().enabled = true;
			PlayerCanvas.enabled = false;
			GameOverCanvas.enabled = true;
			Destroy (gameObject);
		}
		PlayerHPtext.text = PlayerHealthPoints.ToString ();
	}
	#endregion

	private void Heal(){
		if (HealingTime < HealingRate|| PlayerHealthPoints == 100f || PlayerIsShooping != false) {
			//Debug.Log ("Can't heal");
			return;
		}

		IsHealing = true;

		//Debug.Log ("Healing");

		if (IsHealing != false) {
			StartCoroutine (Healing ());
		}

	
			
		}
		

	IEnumerator Healing(){
		while (PlayerHealthPoints < 100f) {
			HealingScreen.color = flashcolor;
			PlayerHealthPoints += HealingAmount + (HealingAmountTime * Time.deltaTime);
			PlayerHPtext.text = PlayerHealthPoints.ToString();
			if (PlayerHealthPoints >= 100f) {
				HealingTime = 0f;
				HealthSlider.value = HealingTime;
				if (PlayerHealthPoints > 100f) {
					PlayerHealthPoints = 100f;
				}
				//Debug.Log ("Done Healing");
				PlayerHPtext.text = PlayerHealthPoints.ToString ();
				IsHealing = false;
				StopCoroutine (Healing ());
			}
		}
		yield return new WaitForSeconds (5f);

	}


	}



