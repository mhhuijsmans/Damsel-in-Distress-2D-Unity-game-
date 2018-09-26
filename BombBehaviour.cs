using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	public List<GameObject> enemiesInRange;

	public float speed = 2.5f;
	public int damage;
	public Vector3 startPosition;
	public Vector3 targetPosition;

	private float distance;
	private float startTime;

	void Start() {
		startTime = Time.time;
		distance = Vector2.Distance (startPosition, targetPosition);
	}

	void Update() {

		float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

		if (gameObject.transform.position.Equals(targetPosition)) {

			foreach (GameObject enemy in enemiesInRange) {
				Transform healthBarTransform = enemy.transform.Find("HealthBar");
				HealthBar healthBar = 
					healthBarTransform.gameObject.GetComponent<HealthBar>();
				healthBar.currentHealth -= damage;

				if (healthBar.currentHealth <= 0) {
					Destroy(enemy);
				}
			}


			Destroy(gameObject);
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
}
