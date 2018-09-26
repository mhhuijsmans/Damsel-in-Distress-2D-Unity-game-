using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTowerBehaviour : MonoBehaviour {

	private GameObject ProxyParent;

	public List<GameObject> enemiesInRange;

	private float fireRate;
	private float lastShotTime;

	void Start() {
		ProxyParent = gameObject.transform.parent.gameObject;

		enemiesInRange = new List<GameObject>();
		lastShotTime = Time.time;

		fireRate = ProxyParent.GetComponent<TowerData> ().fireRate;
	}

	void Update() {

		if (enemiesInRange.Count > 0) {
			if (Time.time - lastShotTime > fireRate) {

				foreach (GameObject enemy in enemiesInRange) {
					Shoot();
				}

				lastShotTime = Time.time;
			}
		}
	}

	void OnEnemyDestroy(GameObject enemy) {
		enemiesInRange.Remove (enemy);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			enemiesInRange.Add(other.gameObject);
			EnemyDestructionDelegate del =
				other.gameObject.GetComponent<EnemyDestructionDelegate>();
			del.enemyDelegate += OnEnemyDestroy;
		}
	}

	void OnTriggerExit2D (Collider2D other)	{
		if (other.gameObject.tag == "Enemy") {
			enemiesInRange.Remove(other.gameObject);
			EnemyDestructionDelegate del =
				other.gameObject.GetComponent<EnemyDestructionDelegate>();
			del.enemyDelegate -= OnEnemyDestroy;
		}
	}

	void Shoot() {
		GameObject bombPrefab = ProxyParent.GetComponent<RedTowerData>().bomb;

		Vector3 startPosition = gameObject.transform.position;
		Vector3 targetPosition = startPosition;
		targetPosition.y -= 6f;
		startPosition.z = bombPrefab.transform.position.z;
		targetPosition.z = bombPrefab.transform.position.z;

		GameObject newBomb = (GameObject)Instantiate (bombPrefab);
		newBomb.transform.position = startPosition;

		BombBehaviour bombComp = newBomb.GetComponent<BombBehaviour>();
		bombComp.startPosition = startPosition;
		bombComp.targetPosition = targetPosition;
	}
}
