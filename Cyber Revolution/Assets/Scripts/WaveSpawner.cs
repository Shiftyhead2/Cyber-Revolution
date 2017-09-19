﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState {Spawning,Waiting,Counting};
	public Text WaveSpawnerNumber;
	public Text WaveIndicator;
	public Text EnemiesRemaining;
	public string WaveCompletedText;

	[System.Serializable]
	public class Wave {
		
		public string Name;
		public Transform[] Enemy;
		public int Count;
		public float Rate;

		
	}

	public Wave [] waves;
	private int nextWave = 0;

	public Transform[] SpawnPoint;

	public float timeBetweenWaves = 5f;
	public float waveCountDown = 0f;

	private float SearchCountDown = 1f;

	public SpawnState state = SpawnState.Counting;

	void Start(){
		if (SpawnPoint.Length == 0) {
			Debug.LogError ("No spawnpoints found!");
		}

		waveCountDown = timeBetweenWaves;

	}

	void Update(){

		if (state == SpawnState.Waiting) {
			if (!EnemyIsAlive ()) {
				WaveCompleted ();

				
			} else {
				return;
			}

		}

		if (waveCountDown <= 0) {
			if (state != SpawnState.Spawning) {
				//Start spawning wave
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		} else {
			waveCountDown -= Time.deltaTime;
			waveCountDown = Mathf.Clamp (waveCountDown, 0f, Mathf.Infinity);
		}
		WaveSpawnerNumber.text = string.Format ("{0:00.00}", waveCountDown);
	}

	void WaveCompleted(){
		//Debug.Log ("Wave Completed!");

		state = SpawnState.Counting;
		waveCountDown = timeBetweenWaves;
		WaveIndicator.text = WaveCompletedText;
		EnemiesRemaining.text = "0";

		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 0;
			//Debug.Log ("All waves completed! Looping and making more enemies...");
		} else {
			nextWave++;
		}
	
	}

	bool EnemyIsAlive(){
		SearchCountDown -= Time.deltaTime;

		if (SearchCountDown <= 0) {
			SearchCountDown = 1f;
			if (GameObject.FindGameObjectWithTag ("Enemy") == null) {
				return false;
			} 
		}
		return true;	
	}

	IEnumerator SpawnWave(Wave _wave){
		//Debug.Log ("Spawning wave:" + _wave.Name);
		state = SpawnState.Spawning;

		WaveIndicator.text = _wave.Name;
		EnemiesRemaining.text = _wave.Count.ToString ();

		//Spawn
		for(int i = 0; i < _wave.Count; i++){
			Transform _enemies = _wave.Enemy [Random.Range (0, _wave.Enemy.Length)];
			SpawnEnemy (_enemies);
			yield return new WaitForSeconds (1f / _wave.Rate);
		}

		state = SpawnState.Waiting;

		yield break;
	}

	void SpawnEnemy(Transform _enemy){

		//Debug.Log ("Spawning enemy:" + _enemy.name);
		Transform _sp = SpawnPoint [Random.Range (0, SpawnPoint.Length)];
		Instantiate (_enemy, _sp.position, _sp.rotation);
	}

}
