using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTowerBehaviour : MonoBehaviour {
	
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
		GameObject target = null;

		float minimalEnemyDistance = float.MaxValue;
		foreach (GameObject enemy in enemiesInRange) {
			float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
			if (distanceToGoal < minimalEnemyDistance) {
				target = enemy;
				minimalEnemyDistance = distanceToGoal;
			}
		}

		if (target != null) {
			if (Time.time - lastShotTime > fireRate) {
				Shoot(target.GetComponent<Collider2D>());
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

	void Shoot(Collider2D target) {
		GameObject bulletPrefab = ProxyParent.GetComponent<GreenTowerData>().bullet;

		Vector3 startPosition = gameObject.transform.position;
		Vector3 targetPosition = target.transform.position;
		startPosition.z = bulletPrefab.transform.position.z;
		targetPosition.z = bulletPrefab.transform.position.z;

		GameObject newBullet = (GameObject)Instantiate (bulletPrefab);
		newBullet.transform.position = startPosition;

		BulletBehaviour bulletComp = newBullet.GetComponent<BulletBehaviour>();
		bulletComp.target = target.gameObject;
		bulletComp.startPosition = startPosition;
		bulletComp.targetPosition = targetPosition;
	}
}