using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderMale : MonoBehaviour {

	private Button btn;

	void Start() {
		btn = GetComponent<Button>();
		btn.onClick.AddListener(() => playSound());
	}

	void playSound() {
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.PlayOneShot(audioSource.clip);
	}
}
