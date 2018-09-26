using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

	private bool inBetweenWaves;
	private bool genNumberDone;
	public GameObject materialCratePrefab;
	public GameObject materialWoodPrefab;
	public GameObject materialBrickPrefab;
	public Vector3[] spawnPointsCrate;
	public Vector3[] spawnPointsWood;
	public Vector3[] spawnPointsBrick;

	public Text materialsLabel;
	public int materials;

	public int Materials {
		get { 
			return materials;
		}
		set {
			materials = value;
			materialsLabel.GetComponent<Text>().text = "Materials: " + materials;
		}
	}


	public Text waveLabel;
	public GameObject[] nextWaveLabels;

	public bool gameOver = false;

	private int wave;
	public int Wave	{
		get {
			return wave;
		}
		set {
			wave = value;
			if (!gameOver)
			{
				for (int i = 0; i < nextWaveLabels.Length; i++)
				{
					nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
				}
			}
			waveLabel.text = "Wave: " + (wave + 1);
		}
	}

	public Text healthLabel;

	public int health;
	public int Health {
		get {
			return health;
		}
		set {
			health = value;
			healthLabel.text = "Health: " + health;

			if (health <= 0 && !gameOver) {
				gameOver = true;
				GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			}
		}
	}

	void Start() {
		Materials = materials;
		Wave = 0;
		Health = health;
		genNumberDone = false;
	}

	void FixedUpdate() {
		inBetweenWaves = GameObject.Find ("Road").GetComponent<SpawnEnemy> ().inBetweenWaves;
		if (inBetweenWaves == true && genNumberDone == false) {
			spawnMaterials (materialCratePrefab, spawnPointsCrate, genSpawnNumber ());
			spawnMaterials (materialWoodPrefab, spawnPointsWood, genSpawnNumber ());
			spawnMaterials (materialBrickPrefab, spawnPointsBrick, genSpawnNumber ());


			genNumberDone = true;
		} 
		else if (inBetweenWaves == false && genNumberDone == true) {
			genNumberDone = false;
		}
	}

	int genSpawnNumber() {
		int number = Random.Range (((Wave + 1) * 2 - 2), ((Wave + 1) * 2));
		return number;
	}

	void spawnMaterials(GameObject prefab, Vector3[] position, int index) {
		GameObject newMaterial = (GameObject) Instantiate (prefab);
		newMaterial.transform.position = position[index];
	}
}
