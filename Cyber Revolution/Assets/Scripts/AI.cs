using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
	
	#region Variables
	public NavMeshAgent myAgent;
	public Transform DestinationPoint;
	public Animator MyAnim;
	public AudioSource MyAudio;
	public AudioClip[] ChaseClips;
	public AudioClip[] AttackClips;
	public float AudioTime;
	public float AudioDelay;
	public float MoveSpeed;

	public bool chaseTarget = true;
	public float stoppingDistance = 2f;




	public float damage = 10f;
	public float damageRate;
	public float DamageDelay = 1.5f;

	private float DistanceFromTarget;


	GameObject thePlayer;
	PlayerHP PlayerHealth;

	#endregion

	void Awake(){
		myAgent = GetComponent<NavMeshAgent> ();
		myAgent.stoppingDistance = stoppingDistance;
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		MyAnim = GetComponent<Animator> ();
		MyAudio = GetComponent<AudioSource> ();
		AudioDelay = Random.Range (10, 30);

		myAgent.speed = MoveSpeed;

		damageRate = Time.time;
	}
		



	#region AI functions
	// Update is called once per frame
	void Update () {
		if (MyAudio.enabled == false) {
			//Don't spam me with warning messages
		}

		float speed = myAgent.velocity.magnitude / myAgent.speed;
		MyAnim.SetFloat ("SpeedPercent", speed, .1f, Time.deltaTime);


		if (AudioTime < AudioDelay) {
			AudioTime += Time.deltaTime;
		}

		if (thePlayer == null) {
			//Don't spam me error messages the player is dead
		}else{
			PlayerHealth = thePlayer.GetComponent<PlayerHP> ();
			DestinationPoint = thePlayer.transform;
			if (thePlayer ==  null) {
				//Do nothing. The Player Does Not Exist
			}else{
				Chase ();
			}
		}

	}

	void Chase(){
		if (thePlayer != null) {
			DistanceFromTarget = Vector3.Distance (DestinationPoint.position, transform.position);
		} else {
			//Wait. Don't Spam. The Player is probably dead since I can't find him.

		}

		if (DistanceFromTarget >= stoppingDistance) {
			chaseTarget = true;
		} else {
			chaseTarget = false;
			Attack ();
		}
		if (chaseTarget) {
			myAgent.SetDestination (DestinationPoint.position);
			MyAnim.SetFloat ("AttackPercent",0f, .1f, Time.deltaTime);
			if (!MyAudio.isPlaying && AudioTime > AudioDelay ) {
				MyAudio.clip = ChaseClips [(Random.Range (0, ChaseClips.Length - 1))];
				MyAudio.Play ();
				AudioTime = 0f;
				AudioDelay = Random.Range (10, 50);

			}
		} else {
			
		}

	}

	void Attack(){
		if (Time.time > damageRate) {
			MyAnim.SetTrigger ("Attack");
			MyAnim.SetFloat ("AttackPercent", Random.Range (0f, 3f), .1f, Time.deltaTime);
			if (!MyAudio.isPlaying && MyAudio != null) {
				MyAudio.clip = AttackClips [(Random.Range (0, AttackClips.Length - 1))];
				MyAudio.Play ();
				AudioTime = 0f;
			}
			PlayerHealth.ApplyPlayerDamage (damage);
			damageRate = Time.time + DamageDelay;
		}
		}






	#endregion
}
