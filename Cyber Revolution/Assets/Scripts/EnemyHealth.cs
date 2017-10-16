using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	#region Variables
	[Header("Enemy Health variable.")]
	public float StartHealth = 100f;
	public float CurrentHealth;
	[SerializeField] private float ArmorPoints;
	public GameObject GameManager;
	[SerializeField] private int MinCost;
	[SerializeField] private int MaxCost;
	[SerializeField] private int Cost;
	public AudioSource Audio;
	public AudioClip[] HurtClips;
	public Transform Corpse;

	[Header("UI")]
	public Image HealthBar;
	public Image HealthBarSlider;
	public GameObject MoneyGained;
	public Text MoneyGainedText;

	[SerializeField] private Animator TextAnimator;

	#endregion

	void Awake(){
		HealthBarSlider.enabled = false;
		HealthBar.enabled = false;
		GameManager =  GameObject.FindGameObjectWithTag ("GameManager");
		MoneyGained = GameObject.Find ("MoneyGained");
		MoneyGainedText = MoneyGained.GetComponent<Text> ();
		TextAnimator = MoneyGained.GetComponent<Animator> ();
		Cost = Random.Range (MinCost, MaxCost);
	
		Audio = GetComponent<AudioSource> ();
		CurrentHealth = StartHealth;


	}

	void Update(){
		if (CurrentHealth <= 0f) {
			Die ();
		}
	}



	#region ApplyDamage
	public void ApplyDamage(float damage){
		HealthBarSlider.enabled = true;
		HealthBar.enabled = true;


		if (!Audio.isPlaying && Audio != null) {
			Audio.clip = HurtClips[Random.Range(0,HurtClips.Length)];
			Audio.Play();
		}
		if (ArmorPoints != 0f) {
			CurrentHealth-= damage - ArmorPoints;
			HealthBar.fillAmount = CurrentHealth / StartHealth;
		} else {
			CurrentHealth -= damage;
			HealthBar.fillAmount = CurrentHealth / StartHealth;
		}
	}

	private void Die(){
		Destroy (gameObject);
		Instantiate (Corpse, this.gameObject.transform.position, this.gameObject.transform.rotation);
		TextAnimator.CrossFadeInFixedTime ("Gain Animation", 0.1f);
		MoneyGainedText.text = "+" + Cost.ToString () + "$";
		GameManager.GetComponent<CurrencyManager> ().Money += Cost;
	}
	#endregion
}
