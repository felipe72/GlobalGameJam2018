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


	bool normal = true;

	void Start(){
		normalWindow.DOLocalMoveX (400, 1f);
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

	}
}
