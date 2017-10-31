using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	#region Variables
	[Header("Enemy Health variable.")]
	public float StartHealth = 100f;
	public float CurrentHealth;
	public float HalfHealth;
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

	void Start(){
		Audio = GetComponent<AudioSource> ();
		GameManager =  GameObject.FindGameObjectWithTag ("GameManager");
		if (MoneyGained != null) {
			MoneyGainedText = MoneyGained.GetComponent<Text> ();
			TextAnimator = MoneyGained.GetComponent<Animator> ();
		}
		HealthBarSlider.enabled = false;
		HealthBar.enabled = false;
		Cost = Random.Range (MinCost, MaxCost);
		CurrentHealth = StartHealth;
		HalfHealth = StartHealth / 2f;


	}

	void Update(){
		if (MoneyGained == null) {
			MoneyGained = GameObject.Find ("MoneyGained");
			if (MoneyGained != null) {
				MoneyGainedText = MoneyGained.GetComponent<Text> ();
				TextAnimator = MoneyGained.GetComponent<Animator> ();
			}

		}
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
			damage -= ArmorPoints;
			damage = Mathf.Clamp (damage, 0, int.MaxValue);
			CurrentHealth -= damage;
			HealthBar.fillAmount = CurrentHealth / StartHealth;
		} else {
			damage = Mathf.Clamp (damage, 0, int.MaxValue);
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
