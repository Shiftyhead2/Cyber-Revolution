using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

	public Rigidbody MyRB;

	public float maxForce;
	public float minForce;


	float lifetime = 10f;
	float fadetime = 2f;

	// Use this for initialization
	void Start () {
		float force = Random.Range (minForce, maxForce);
		MyRB.AddForce (transform.right * force);
		MyRB.AddTorque (Random.insideUnitSphere * force);
		StartCoroutine (Fade ());

	}
	
	IEnumerator Fade(){
		yield return new WaitForSeconds (lifetime);

		float percent = 0f;
		float fadespeed = 1f / fadetime;
		Material mat = GetComponent<Renderer> ().material;
		Color initialColor = mat.color;

		while (percent < 1f) {
			percent += Time.deltaTime * fadespeed;
			mat.color = Color.Lerp (initialColor, Color.clear, percent);
			yield return null;
		}
		Destroy (gameObject);

	}
}
