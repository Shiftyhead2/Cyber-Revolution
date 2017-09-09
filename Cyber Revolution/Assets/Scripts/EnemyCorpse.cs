using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCorpse : MonoBehaviour {

	private AudioSource CorpseAudio;
	public AudioClip[] DeathClips;
	public float DestroyTime;

	// Use this for initialization
	void Start () {
		CorpseAudio = GetComponent<AudioSource> ();
		if (!CorpseAudio.isPlaying && CorpseAudio != null) {
			CorpseAudio.clip = DeathClips [Random.Range (0, DeathClips.Length - 1)];
			CorpseAudio.Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (this.gameObject, DestroyTime);
	}
}
