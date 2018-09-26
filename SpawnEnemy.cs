using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]

public class Wave {
	public GameObject[] enemyPrefabs;
	public float spawnInterval;
}


public class SpawnEnemy : MonoBehaviour {
	
	public Text timeLabel;
	public int timeBetweenWaves;

	public bool inBetweenWaves;

	public GameObject[] waypoints;

	public Wave[] waves;


	private GameManagerBehaviour gameManager;

	private float lastSpawnTime;
	private int enemiesSpawned = 0;
    
	void Start () {
		inBetweenWaves = true;
		lastSpawnTime = Time.time;
		gameManager =
			GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
		Invoke ("playSound", timeBetweenWaves - 3);
	}

	void Update () {

		int currentWave = gameManager.Wave;

		if (currentWave < waves.Length) {
			int numOfEnemies = waves [currentWave].enemyPrefabs.Length;
			float spawnInterval = waves [currentWave].spawnInterval;

			float timeInterval = Time.time - lastSpawnTime;
			string timeLabelStr;
			if (inBetweenWaves == true) {
				timeLabelStr = (timeBetweenWaves - Mathf.Round(timeInterval)).ToString();
			} 
			else {
				timeLabelStr = "0";
			}
			timeLabel.text = "Time: " + timeLabelStr;



			foreach (GameObject enemy in waves[currentWave].enemyPrefabs) {
				if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves && inBetweenWaves == true) ||
				    (enemiesSpawned > 0 && timeInterval > spawnInterval)) &&
				    enemiesSpawned < numOfEnemies) {
					inBetweenWaves = false;

					GameObject newEnemy = (GameObject)
						Instantiate (waves [currentWave].enemyPrefabs [enemiesSpawned]);
					
					newEnemy.GetComponent<MoveEnemy> ().waypoints = waypoints;

					lastSpawnTime = Time.time;
					timeInterval = Time.time - lastSpawnTime;
					enemiesSpawned++;
				}
			}

			if (enemiesSpawned == numOfEnemies &&
				GameObject.FindGameObjectWithTag("Enemy") == null) {
				enemiesSpawned = 0;
				lastSpawnTime = Time.time;
				timeInterval = 0;
				gameManager.Wave++;
				inBetweenWaves = true;
				Invoke ("playSound", timeBetweenWaves - 3);

			}
		}
		else {
			gameManager.gameOver = true;
			GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");
			gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
		}
	}

	void playSound() {
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.PlayOneShot(audioSource.clip);
	}
}
