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
			CardsManager _cards = CardsManager.Instance;

			var x = _cards.cardsOnDeckStack.Concat (_cards.cardsOnDiscardStack).Concat (_cards.cardsOnHand).Select (y => y.GetComponent<Card> ());
			/*foreach (var y in x) {
				var z = Instantiate (y).GetComponent<Card> ();
				DontDestroyOnLoad (z.gameObject);
				PersistingData.Instance.cards.Add (z);
			}
			_cards.enabled = false;
			CardsManager.Instance = null;
			MapManager.Instance = null;
			TurnManager.Instance = null;
			*/

			CardsManager.Instance.backGround.DOFade (1f, 1f).OnComplete(() => {
				CardsManager.Instance.window.transform.DOLocalMoveY(0, 1f).OnComplete(() => {
					CardButton[] cardsButtons = CardsManager.Instance.hori.GetComponentsInChildren<CardButton>();

					for(int i=0; i<3; i++){
						var card = CardsManager.Instance.newCards[Random.Range(0, CardsManager.Instance.newCards.Count)].GetComponent<Card>();
						cardsButtons[0].gameObject.SetActive(true);
						cardsButtons[0].card = card;

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
