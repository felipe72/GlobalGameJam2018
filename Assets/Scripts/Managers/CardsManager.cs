using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardsManager : Singleton<CardsManager> {

	public List<GameObject> cardsAvailable;

	public List<GameObject> cardsOnHand;
	public List<GameObject> cardsOnDeckStack;
	public List<GameObject> cardsOnDiscardStack;

	public List<GameObject> cardsOnExecutionStack;

	[Header("Info Variables")]
	public int actionsAmount;
	public int deckAmount = 12;

	[Header("Canvas Positions")]
	public GameObject handCards;
	public GameObject discardCards;
	public GameObject deckCards;

	public Image[] symbolSlots;

	void Awake()
	{
		InitDeck ();
		RefreshHandCards();
		actionsAmount = 0;
	}

	/*void RefreshDeckCards()
	{
		
		RefreshHandCards ();
	}*/

	void RefreshHandCards()
	{
		int currentAmount = cardsOnHand.Count;
		for (int i = 0; i < 5 - currentAmount; i++) 
		{
			if (cardsOnDeckStack.Count == 0) 
			{
				for (int j = 0; j < cardsOnDiscardStack.Count; j++) 
				{
					GameObject _card = cardsOnDiscardStack [i];
					Card cardComp = _card.GetComponent<Card> ();

					_card.transform.SetParent (deckCards.transform);
					_card.transform.localPosition = new Vector3 (0, 0, 0);
					cardsOnDeckStack.Add(_card);
					cardsOnDiscardStack.Remove (_card);
				}
				ShuffleDeck (3);
			}
				
			int cleverton = cardsOnDeckStack.Count - 1;	
			print (cardsOnDeckStack.Count.ToString () + " " + cleverton.ToString ());
			GameObject _cardjooj = cardsOnDeckStack [cleverton];
			if (_cardjooj) 
			{
				Card cardComp = _cardjooj.GetComponent<Card> ();
				_cardjooj.transform.SetParent (handCards.transform);
				cardsOnDeckStack.Remove (_cardjooj);
				cardsOnHand.Add (_cardjooj);
				cardComp.RotateToFront ();
				Debug.Log ("jooj");
				//yield return new WaitForSeconds (0.1f);
			}
		}
	}

	void ShuffleDeck(int amount)
	{
		for(int kek = 0 ; kek < amount ; kek++)
			for (int i = 0; i < cardsOnDeckStack.Count; i++) 
			{
				int rand = Random.Range (0, cardsOnDeckStack.Count);
				GameObject _aux = cardsOnDeckStack [rand];
				cardsOnDeckStack [rand] = cardsOnDeckStack [i];
				cardsOnDeckStack [i] = _aux;
			}
	}

	void InitDeck()
	{
		for (int i = 0; i < cardsAvailable.Count; i++) 
		{
			GameObject goCard =  Instantiate (cardsAvailable [i],deckCards.transform);
			cardsOnDeckStack.Add (goCard);
			goCard.transform.localPosition = new Vector3 (0, 0, 0);
		}
		ShuffleDeck (3);
	}

	public void AddToExecutionStack(Card _card)
	{
		if (!_card.isAlreadySelected) 
		{
			if (actionsAmount < 3) 
			{
				cardsOnExecutionStack.Add (_card.gameObject);
				actionsAmount++;
				_card.isAlreadySelected = !_card.isAlreadySelected;
				RefreshImageSymbols ();

			}
		} else {
			cardsOnExecutionStack.Remove (_card.gameObject);
			actionsAmount--;
			_card.isAlreadySelected = !_card.isAlreadySelected;
			RefreshImageSymbols ();
		}

		TurnManager.Instance.startTurnButton.interactable = (actionsAmount >= 1);

	}

	void RefreshImageSymbols()
	{
		for (int i = 0; i < 3; i++) 
		{
			symbolSlots [i].sprite = null;
		}
		for (int i = 0; i < cardsOnExecutionStack.Count; i++) 
		{
			Card _card = cardsOnExecutionStack [i].GetComponent<Card> ();
			symbolSlots [i].sprite = _card.symbol;
		}
	}

	public void StartTurn()
	{
		StartCoroutine (ExecuteTurn ());
	}

	IEnumerator ExecuteTurn()
	{
		TurnManager.Instance.SetHandCards (false);
		TurnManager.Instance.startTurnButton.interactable = false;
		for (int i = 0; i < cardsOnExecutionStack.Count; i++) 
		{
			GameObject _card = cardsOnExecutionStack [i];
			Card cardComp = _card.GetComponent<Card> ();

			_card.transform.SetParent (discardCards.transform);
			_card.transform.localPosition = new Vector3 (0, 0, 0);
			cardComp.RotateToBack ();

			cardsOnHand.Remove (_card);
			cardsOnDiscardStack.Add (_card);
		}

		yield return new WaitForSeconds (1f);
		for (int i = 0; i < cardsOnExecutionStack.Count; i++) 
		{
			Card _currentCard = cardsOnExecutionStack [i].GetComponent<Card> ();
			_currentCard.Execute ();
			yield return new WaitForSeconds (1f);
		}

		cardsOnExecutionStack.Clear ();
		RefreshImageSymbols ();
		actionsAmount = 0;

		//Enemy Turn

		List<Enemy> enemies = new List<Enemy>();
		foreach (Enemy enemy in FindObjectsOfType<Enemy> ()) 
		{
			enemies.Add (enemy);
		}
		for (int j = 0; j < 3; j++) 
		{
			for (int i = 0; i < enemies.Count; i++) 
			{
				enemies [i].DoAI ();
			}
			yield return new WaitForSeconds (1f);
		}

		RefreshHandCards ();
		yield return new WaitForSeconds (0.1f);
		TurnManager.Instance.SetHandCards (true);
		TurnManager.Instance.SetAlreadyTaken (false);
	}




}
