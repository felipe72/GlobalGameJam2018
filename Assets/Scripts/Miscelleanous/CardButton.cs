using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardButton : MonoBehaviour {
	public Card card;
	public Button button;

	public void Click(){
		button.gameObject.SetActive (true);
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
		CardsManager _cards = CardsManager.Instance;

		var x = _cards.cardsOnDeckStack.Concat (_cards.cardsOnDiscardStack).Concat (_cards.cardsOnHand).Select (y => y.GetComponent<Card> ()).ToList();

		print (x.Find (a => a.action == card.action));

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
