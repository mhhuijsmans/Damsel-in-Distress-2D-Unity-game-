// http://answers.unity.com/questions/572176/how-can-i-instantiate-a-gameobject-directo-into-a-1.html
// https://answers.unity.com/questions/938496/buttononclickaddlistener.html
// https://forum.unity.com/threads/calling-onclick-from-a-script.426174/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour {

	private GameManagerBehaviour gameManager;

	public GameObject PrefabTowerBlue1;
	public GameObject PrefabTowerBlue2a;
	public GameObject PrefabTowerBlue2b;
	public GameObject PrefabTowerBlue3a;
	public GameObject PrefabTowerBlue3b;
	public GameObject PrefabTowerGreen1;
	public GameObject PrefabTowerGreen2a;
	public GameObject PrefabTowerGreen2b;
	public GameObject PrefabTowerGreen3a;
	public GameObject PrefabTowerGreen3b;
	public GameObject PrefabTowerRed1;
	public GameObject PrefabTowerRed2;
	public GameObject PrefabTowerRed3;

	public GameObject PrefabButtonBlue1;
	public GameObject PrefabButtonBlue2;
	public GameObject PrefabButtonBlue3;
	public GameObject PrefabButtonGreen1;
	public GameObject PrefabButtonGreen2;
	public GameObject PrefabButtonGreen3;
	public GameObject PrefabButtonRed1;
	public GameObject PrefabButtonRed2;
	public GameObject PrefabButtonRed3;

	public GameObject[] ButtonList;

	private GameObject TowerMenu;
	private int ButtonNo;
	private GameObject Button;
	private GameObject ProxyTower;
	private GameObject NewTower;

	private float PosX;
	private float PosY;
	private float Pos1 = -2;
	private float Pos2 = -1;
	private float Pos3 = 0;
	private float Pos4 = 1;
	private float Pos5 = 2;

	string tp = "Tower Platform";
	string bt1 = "Magnet Tower 1";
	string bt2a = "Magnet Tower 2a";
	string bt2b = "Magnet Tower 2b";
	string bt3a = "Magnet Tower 3a";
	string bt3b = "Magnet Tower 3b";
	string gt1 = "Venus Tower 1";
	string gt2a = "Venus Tower 2a";
	string gt2b = "Venus Tower 2b";
	string gt3a = "Venus Tower 3a";
	string gt3b = "Venus Tower 3b";
	string rt1 = "Pussy Hat Tower 1";
	string rt2 = "Pussy Hat Tower 2";
	string rt3 = "Pussy Hat Tower 3";

	void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehaviour> ();

		PosX = gameObject.transform.position.x;
		PosY = gameObject.transform.position.y;

		TowerMenu = transform.Find ("TowerMenu").gameObject;
		TowerMenu.SetActive (false);

		switch (GetTowerType ()) {
		case 1:
			CreateButton(1, PrefabButtonBlue1, Pos1, PrefabTowerBlue1);
			CreateButton(2, PrefabButtonGreen1, Pos3, PrefabTowerGreen1);
			CreateButton(3, PrefabButtonRed1, Pos5, PrefabTowerRed1);
			break;
		case 2:
			CreateButton(1, PrefabButtonBlue2, Pos2, PrefabTowerBlue2a);
			CreateButton(2, PrefabButtonBlue2, Pos4, PrefabTowerBlue2b);
			break;
		case 3:
			CreateButton(1, PrefabButtonGreen2, Pos2, PrefabTowerGreen2a);
			CreateButton(2, PrefabButtonGreen2, Pos4, PrefabTowerGreen2b);
			break;
		case 4:
			CreateButton(1, PrefabButtonRed2, Pos3, PrefabTowerRed2);
			break;
		case 5:
			CreateButton(1, PrefabButtonBlue3, Pos3, PrefabTowerBlue3a);
			break;
		case 6:
			CreateButton(1, PrefabButtonBlue3, Pos3, PrefabTowerBlue3b);
			break;
		case 7:
			CreateButton(1, PrefabButtonGreen3, Pos3, PrefabTowerGreen3a);
			break;
		case 8:
			CreateButton(1, PrefabButtonGreen3, Pos3, PrefabTowerGreen3b);
			break;
		case 9:
			CreateButton(1, PrefabButtonRed3, Pos3, PrefabTowerRed3);
			break;

		default:
			break;

		}
	}

	void Update() {
		foreach (GameObject Button in ButtonList) {
			if (Button.activeInHierarchy == true) {				
				ButtonNo = Button.GetComponent<ButtonData> ().ButtonNo;
				if (Input.GetButtonDown ("Fire" + ButtonNo)) {
					Button.GetComponent<Button> ().onClick.Invoke ();
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			TowerMenu.SetActive (true);
			ButtonList = GameObject.FindGameObjectsWithTag("Button");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			TowerMenu.SetActive (false);
		}
	}

	void CreateButton(int ButtonNo, GameObject ButtonPrefab, float Pos, GameObject TowerType) {
		Button = (GameObject)
			Instantiate (ButtonPrefab);
		Button.transform.SetParent (TowerMenu.transform, false);
		Button.GetComponent<RectTransform> ().localPosition = new Vector3 (PosX + Pos, PosY - 3, 0);
		Button.GetComponent<Button> ().onClick.AddListener (() => CreateTower (TowerType));
		Button.GetComponent<ButtonData> ().ButtonNo = ButtonNo;
	}

	void CreateTower(GameObject TowerType) {
		if (gameManager.Materials >= TowerType.GetComponent<TowerData> ().cost) {
			gameManager.Materials -= TowerType.GetComponent<TowerData> ().cost;
			NewTower = (GameObject)
				Instantiate (TowerType, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	int GetTowerType() {
		var tower = gameObject.name;

		if (tower.Contains(tp)) {
			return 1;
		}
		if (tower.Contains(bt1)) {
			return 2;
		}
		if (tower.Contains(gt1)) {
			return 3;
		}
		if (tower.Contains(rt1)) {
			return 4;
		}
		if (tower.Contains(bt2a)) {
			return 5;
		}
		if (tower.Contains(bt2b)) {
			return 6;
		}
		if (tower.Contains(gt2a)) {
			return 7;
		}
		if (tower.Contains(gt2b)) {
			return 8;
		}
		if (tower.Contains(rt2)) {
			return 9;
		}
		return 0;
	}
}
