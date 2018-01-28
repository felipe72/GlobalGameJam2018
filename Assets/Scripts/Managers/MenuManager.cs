using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour {
	public RectTransform normalWindow;
	public RectTransform rankWindow;

	public Image normalImg;
	public Image rankImg;

	public GameObject names;
	public GameObject missions;

	void Start(){
		PlayerPrefs.SetInt ("currentMission", 0);
		normalWindow.DOLocalMoveX (400, 1f);

		Rank ();
	}

	public void ActivateRank(){
		rankImg.DOFade (1f, 1f);
		normalImg.DOFade (0f, 1f);

		normalWindow.DOLocalMoveX (900, 1f).OnComplete (() => {
			rankWindow.DOLocalMoveX (0, 1f);
		});
	}

	public void ActivateNormal(){
		rankImg.DOFade (0f, 1f);
		normalImg.DOFade (1f, 1f);

		rankWindow.DOLocalMoveX (-1280, 1f).OnComplete (() => {
			normalWindow.DOLocalMoveX (400, 1f);;
		});
	}

	public void LoadGame(){
		LoadingScreenManager.LoadScene (2);
	}

	public void Exit(){
		Application.Quit ();
	}

	public void Rank(){
		var _names = names.GetComponentsInChildren<Text> ();
		var _missions = missions.GetComponentsInChildren<Text> ();

		for (int i = 0; i < 10; i++) {
			int amount = PlayerPrefs.GetInt (i.ToString () + "place");
			string name = PlayerPrefs.GetString (i.ToString () + "name");
			string s = string.Format ("{0}.{1}", (i+1).ToString(), name);

			_names [i].text = s;
			_missions [i].text = amount.ToString ();
		}
	}
}
