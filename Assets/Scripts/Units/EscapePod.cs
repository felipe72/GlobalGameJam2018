using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class EscapePod : MovingUnit {
	//public Card[] cards;

	Pickup[] pickups;

	new void Start(){
		base.Start ();

		Initialize ();
	}

	void Initialize(){
		pickups = FindObjectsOfType<Pickup> ();

		currentTile.onTileEnter += CheckEnd;
	}

	void CheckEnd(MovingUnit unit){
		pickups = pickups.Where (x => x != null).ToArray();

		if (unit.GetComponent<Player> () != null && pickups.Length == 0) {
			CardsManager.Instance.backGround.DOFade (1f, 1f).OnComplete(() => {
				CardsManager.Instance.window.transform.DOLocalMoveY(0, 1f).OnComplete(() => {
					CardButton[] cardsButtons = CardsManager.Instance.hori.GetComponentsInChildren<CardButton>();

					for(int i=0; i<3; i++){
						var card = CardsManager.Instance.newCards[Random.Range(0, CardsManager.Instance.newCards.Count)].GetComponent<Card>();
						cardsButtons[i].GetComponent<Image>().color = Color.white;
						cardsButtons[i].card = card;
						cardsButtons[i].GetComponent<Image>().sprite = card.sprite;
					}
				});
			});

			//LoadingScreenManager.LoadScene (2);
			//StartCoroutine(CardsManager.Instance.StartPosStageScreen());

		}
	}

	IEnumerator test(){
		yield return null;
		LoadingScreenManager.LoadScene (2);
	}

	override protected void UpdateAnim(){

	}
}
