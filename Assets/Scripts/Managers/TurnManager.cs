using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : Singleton<TurnManager> {

	public bool isPLayerTurn = true;
	public bool isEnemyTurn = false;
	public bool isAnimationTime = false;

	public Button startTurnButton;

	public void SetHandCards(bool state)
	{
		for (int i = 0; i < CardsManager.Instance.cardsOnHand.Count; i++) 
		{
			Button _button = CardsManager.Instance.cardsOnHand [i].GetComponent<Button> ();
			_button.interactable = state;
		}
	}

	public void SetAlreadyTaken(bool state)
	{
		for (int i = 0; i < CardsManager.Instance.cardsOnHand.Count; i++) 
		{
			Card _card = CardsManager.Instance.cardsOnHand [i].GetComponent<Card> ();
			_card.isAlreadySelected = state;
		}
	}

	void Start()
	{
		SetHandCards (true);
	}

}
