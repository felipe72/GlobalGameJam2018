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

	[Header("Canvas Positions")]
	public GameObject handCards;
	public GameObject discardCards;
	public GameObject deckCards;

	public Image[] symbolSlots;

	void Awake()
	{
		RefreshDeckCards ();
		actionsAmount = 0;
	}

	void RefreshDeckCards()
	{
		for (int i = 0; i < 12; i++) 
		{
			int index = Random.Range (0, cardsAvailable.Count);	
			GameObject _card = Instantiate (cardsAvailable [index], deckCards.transform);
			cardsOnDeckStack.Add(_card);
		}
		RefreshHandCards ();
	}

	void RefreshHandCards()
	{
		for (int i = 0; i < 5; i++) 
		{
			int index = cardsOnDeckStack.Count-1;	
			GameObject _card = cardsOnDeckStack[index];
			if (_card) 
			{
				_card.transform.SetParent (handCards.transform);
				cardsOnDeckStack.Remove (_card);
				cardsOnHand.Add (_card);
			}
		}
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
		for (int i = 0; i < cardsOnExecutionStack.Count-1; i++) 
		{
			Card _currentCard = cardsOnExecutionStack [i].GetComponent<Card> ();
			_currentCard.Execute ();
			yield return new WaitForSeconds (1f);
		}
	}




}
