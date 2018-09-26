using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTowerData : MonoBehaviour {
	
	public GameObject bomb;
	
	private Animator anim;

	void Start() {
		if (GetComponent<Animator> () != null) {
			anim = GetComponent<Animator> ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (anim != null && other.gameObject.tag == "Player") {
			anim.SetTrigger ("Active");
		}
	}
}
