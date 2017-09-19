using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	#region Variables
	[Header("Enemy Health variable.")]
	public float health = 100f;
	[SerializeField] private float ArmorPoints;
	public GameObject GameManager;
	[SerializeField] private int MinCost;
	[SerializeField] private int MaxCost;
	[SerializeField] private int Cost;
	public AudioSource Audio;
	public AudioClip[] HurtClips;
	public Transform Corpse;
	#endregion

	void Awake(){
		GameManager =  GameObject.FindGameObjectWithTag ("GameManager");
		Cost = Random.Range (MinCost, MaxCost);
		Audio = GetComponent<AudioSource> ();
	}

	void Update(){
		if (health <= 0f) {
			Die ();
		}
	}



	#region ApplyDamage
	public void ApplyDamage(float damage){
		if (!Audio.isPlaying && Audio != null) {
			Audio.clip = HurtClips[Random.Range(0,HurtClips.Length-1)];
			Audio.Play();
		}
		if (ArmorPoints != 0f) {
			health -= damage - ArmorPoints;
		} else {
			health -= damage;
		}
	}

	private void Die(){
		Destroy (gameObject);
		Instantiate (Corpse, this.gameObject.transform.position, this.gameObject.transform.rotation);
		GameManager.GetComponent<CurrencyManager> ().Money += Cost;
	}
	#endregion
}
