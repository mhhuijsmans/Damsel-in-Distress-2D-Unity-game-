using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public float maxHealth = 100;
	public float currentHealth = 100;
	private float originalScale;

	void Start() {
		originalScale = transform.localScale.x;
	}

	void Update() {
		Vector3 tmpScale = transform.localScale;
		tmpScale.x = currentHealth / maxHealth * originalScale;
		transform.localScale = tmpScale;
	}
}
