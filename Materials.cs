using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour {

	private GameManagerBehaviour gameManager;
	public int MaterialAmount;

	void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehaviour> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			gameManager.Materials += MaterialAmount;
			playSound ();
			Invoke ("destroy", 0.2f);
		}
	}

	void playSound() {
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.PlayOneShot(audioSource.clip);
	}

	void destroy(){
		Destroy (gameObject);
	}
}
