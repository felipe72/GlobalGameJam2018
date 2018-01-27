using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {
	int currentEnergy = 20;

	public GameObject bar;
	public Color low;
	public Color medium;
	public Color high;

	void Start(){
		UpdateBar ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.J)) {
			int amount = Random.Range (1, 21);
			currentEnergy = amount;
			UpdateBar ();
		}
	}

	void UpdateBar(){
		int k = Mathf.FloorToInt(currentEnergy / 2f);

		var l = bar.GetComponentsInChildren<Image> ();

		foreach (var x in l) {
			x.fillAmount = 1f;
			x.color = GetCurrentColor ();
		}

		for (int i = 0; i < 10 - k; i++) {
			l [9 - i].fillAmount = 0f;
		}

		if (currentEnergy % 2 == 1) {
			l [k].fillAmount = .5f;
		}
	}

	Color GetCurrentColor(){
		Color color = Color.white;

		if (currentEnergy < 20/3f) {
			color = low;
		} else if (currentEnergy < 40/3f) {
			color = medium;
		} else {
			color = high;
		}

		return color;
	}
}
