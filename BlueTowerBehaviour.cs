//https://answers.unity.com/questions/923482/how-to-run-a-function-for-exactly-x-seconds.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTowerBehaviour : MonoBehaviour {

	private GameObject ProxyParent;
	private Animator anim;

	public List<GameObject> enemiesInRange;

	private float fireRate;
	private float lastShotTime;
	public float magnetizedTime;
	public float delayFactor;

	void Start() {
		ProxyParent = gameObject.transform.parent.gameObject;

		if (ProxyParent.GetComponent<Animator> () != null) {
			anim = ProxyParent.GetComponent<Animator> ();
		}

		enemiesInRange = new List<GameObject>();
		lastShotTime = Time.time;

		fireRate = ProxyParent.GetComponent<TowerData> ().fireRate;

		magnetizedTime = ProxyParent.GetComponent<BlueTowerData> ().magnetizedTime;

		delayFactor = ProxyParent.GetComponent<BlueTowerData> ().delayFactor;
	}

	void OnEnemyDestroy(GameObject enemy) {
		enemiesInRange.Remove (enemy);
	}

	void Update() {

		if (enemiesInRange.Count > 0) {
			if (Time.time - lastShotTime > fireRate) {
				if (anim != null) {
					anim.SetTrigger ("Fire");
				}

				foreach (GameObject enemy in enemiesInRange) {
					StartCoroutine(Shoot(enemy));
				}

				lastShotTime = Time.time;
			}
		}
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

	IEnumerator Shoot(GameObject target) {
		float timePassed = 0;
		float oldDelayTime = target.GetComponent<MoveEnemy> ().delayTime;
		while (timePassed < magnetizedTime) {
			timePassed += Time.deltaTime;
			target.GetComponent<MoveEnemy> ().delayTime = oldDelayTime + timePassed;
			yield return null;
		}
		target.GetComponent<MoveEnemy> ().delayTime = oldDelayTime + magnetizedTime;
	}
}
