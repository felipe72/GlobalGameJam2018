using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardsManager : MonoBehaviour {

	public List<GameObject> cardsAvailable;

	public List<GameObject> cardsOnHand;
	public List<GameObject> cardsOnDeckStack;
	public List<GameObject> cardsOnDiscardStack;

	void Awake()
	{
		RefreshHandCards ();
	}

	void RefreshHandCards()
	{
		for (int i = 0; i < 5; i++) 
		{
			int index = Random.Range (0, cardsAvailable.Count);	
			cardsOnHand[i] = cardsAvailable [index];
		}
	}

	public void OnClickCard(int index)
	{
		cardsOnHand [index].GetComponent<Card> ().Execute ();
	}


}
