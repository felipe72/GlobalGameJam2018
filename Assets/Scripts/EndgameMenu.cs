using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class EndgameMenu : MonoBehaviour {
	public GameObject discard;
	public GameObject gain;

	public GameObject _parent;

	public void ActivateDiscard(){

		CardsManager _cards = CardsManager.Instance;

		var x = _cards.cardsOnDeckStack.Concat (_cards.cardsOnDiscardStack).Concat (_cards.cardsOnHand).Select (y => y.GetComponent<Card> ()).ToList();

		var z = _parent.GetComponentsInChildren<CardButton>();

		for (int i = 0; i < z.Length; i++) {
			z [i].gameObject.SetActive (i < x.Count);

			if (i < x.Count) {
				z [i].card = x [i];
				z [i].GetComponent<Image> ().sprite = z [i].card.sprite;
			}

		}
	
		gain.SetActive (false);
		discard.SetActive (true);
	}

	public void DeactivateDiscard(){
		gain.SetActive (true);
		discard.SetActive (false);
	}


}
