using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class CardButton : MonoBehaviour {
	public Card card;
	public Button button;

	public void Click(){
		if (button.gameObject.activeSelf) {
			button.gameObject.SetActive (false);
		} else {
			CardButton[] cards = transform.parent.GetComponentsInChildren<CardButton> ();
			foreach (var card in cards) {
				card.button.gameObject.SetActive (false);
			}

			button.gameObject.SetActive (true);
		}
	}

	public void Confirm(){
		CardsManager.Instance.AddCardToDeck (Instantiate(card));

		CardsManager _cards = CardsManager.Instance;

		var x = _cards.cardsOnDeckStack.Concat (_cards.cardsOnDiscardStack).Concat (_cards.cardsOnHand).Select (y => y.GetComponent<Card> ());
		foreach (var y in x) {
			var z = Instantiate (y).GetComponent<Card> ();
			DontDestroyOnLoad (z.gameObject);
			PersistingData.Instance.cards.Add (z);
		}
		_cards.enabled = false;
		CardsManager.Instance = null;
		MapManager.Instance = null;
		TurnManager.Instance = null;

		LoadingScreenManager.LoadScene (2);
	}

	public void Confirm2(){
		foreach (Transform x in transform) {
			x.gameObject.SetActive (false);
		}

		transform.parent.GetComponent<GridLayoutGroup>().enabled = false;
		transform.DOLocalMoveY (transform.localPosition.y - 200, .5f).OnComplete (() => {
			StartCoroutine(loadScene());
		});
		GetComponent<Image> ().DOFade (0f, .5f);
	}

	IEnumerator loadScene(){
		yield return new WaitForSeconds (1f);

		CardsManager _cards = CardsManager.Instance;

		var x = _cards.cardsOnDeckStack.Concat (_cards.cardsOnDiscardStack).Concat (_cards.cardsOnHand).Select (y => y.GetComponent<Card> ()).ToList();

		x.Remove (x.Find(a => a.action == card.action));

		foreach (var y in x) {
			var z = Instantiate (y).GetComponent<Card> ();
			DontDestroyOnLoad (z.gameObject);
			PersistingData.Instance.cards.Add (z);
		}
		_cards.enabled = false;
		CardsManager.Instance = null;
		MapManager.Instance = null;
		TurnManager.Instance = null;

		LoadingScreenManager.LoadScene (2);
	}
}
