using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardsManager : Singleton<CardsManager> {

	public List<GameObject> cardsAvailable;

	public List<GameObject> cardsOnHand;
	public List<GameObject> cardsOnDeckStack;
	public List<GameObject> cardsOnDiscardStack;

	public List<GameObject> cardsOnExecutionStack;

	[Header("Canvas Positions")]
	public GameObject handCards;
	public GameObject discardCards;
	public GameObject deckCards;

	void Awake()
	{
		RefreshDeckCards ();
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




}
