using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GenderFemale : MonoBehaviour {

	private Button btn;

	void Start() {
		btn = GetComponent<Button>();
		btn.onClick.AddListener(() => play());
	}

	void play() {
		Invoke ("playSound", 0);
		Invoke ("playGame", 1.5f);
	}

	void playSound() {
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.PlayOneShot(audioSource.clip);
	}

	void playGame() {
		SceneManager.LoadScene ("main");
	}
}
