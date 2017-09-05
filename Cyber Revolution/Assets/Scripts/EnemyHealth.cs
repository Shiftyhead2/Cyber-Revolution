using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	#region Variables
	[Header("Enemy Health variable.")]
	public float health = 100f;
	[SerializeField] private float ArmorPoints;
	public GameObject GameManager;
	[SerializeField] private int Cost;
	public AudioSource Audio;
	public AudioClip[] HurtClips;
	#endregion

	void Awake(){
		GameManager =  GameObject.FindGameObjectWithTag ("GameManager");
		Cost = Random.Range (15, 150);
		Audio = GetComponent<AudioSource> ();
	}




	#region ApplyDamage
	public void ApplyDamage(float damage){
		if (!Audio.isPlaying) {
			Audio.clip = HurtClips[Random.Range(0,HurtClips.Length-1)];
			Audio.Play();
		}

		if (ArmorPoints != 0f) {
			health -= damage / ArmorPoints;
		} else {
			health -= damage;
		}
		if (health <= 0f) {
			Destroy (gameObject);
			GameManager.GetComponent<CurrencyManager> ().Money += Cost;

		}
	}
	#endregion
}
