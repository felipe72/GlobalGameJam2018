using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour {
	public Card card;

	public Button button;

	public void Click(){
		button.gameObject.SetActive (true);
	}

	public void Confirm(){
		CardsManager.Instance.AddCardToDeck (card);
	}
}
