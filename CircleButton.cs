//https://answers.unity.com/questions/821613/unity-46-is-it-possible-for-ui-buttons-to-be-non-r.html

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircleButton : MonoBehaviour {

	public float AlphaThreshold = 0.1f;

	void Start()
	{
		this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
	}
}
