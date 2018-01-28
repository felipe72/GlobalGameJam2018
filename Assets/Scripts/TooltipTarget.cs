using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class TooltipTarget : MonoBehaviour {
	Card card;

	// Use this for initialization
	void Start () {
		card = GetComponent<Card> ();

		EventTrigger trigger = GetComponent<EventTrigger> ();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback.AddListener((data) => ActivateTooltip());

		EventTrigger.Entry exit = new EventTrigger.Entry ();
		exit.eventID = EventTriggerType.PointerExit;
		exit.callback.AddListener ((data) => DeactivateTooltip ());

		trigger.triggers.Add (entry);
		trigger.triggers.Add (exit);
	}

	void ActivateTooltip(){
		Tooltip.Instance.content.text = card.description;
		Tooltip.Instance.cardImage.sprite = card.sprite;

		Tooltip.Instance.ShowTooltip ();
	}

	void DeactivateTooltip(){
		Tooltip.Instance.HideTooltip ();
	}
}
