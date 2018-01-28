using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : Singleton<Tooltip> {
	public Text content;
	public Image cardImage;

	public int xLimit;
	public int yLimit;
	public int xOffset;
	public int yOffset;
	public Vector3 offset;

	bool canFollow;

	void Update(){
		if (canFollow) {
			FollowMouse ();
		}

	}

	void FollowMouse(){
		var pos = Input.mousePosition ;

		if (pos.x > xLimit) {
			pos += Vector3.right * xOffset;
		}

		if (pos.y > yLimit) {
			pos += Vector3.up * yOffset;
		}
		RectTransform rect = (RectTransform)transform;
		rect.localPosition = pos + offset;
	}

	public void ShowTooltip(){
		canFollow = true;
	}

	public void HideTooltip(){
		canFollow = false;
		transform.position = Vector3.one * 1000;
	}
}
