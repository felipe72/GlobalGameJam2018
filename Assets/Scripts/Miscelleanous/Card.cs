﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	public string name;
	public string description;
	public int energyCost;
	public Actions action;

	public bool isAlreadySelected = false;

	public void AddToExecutionStack()
	{
		if (!isAlreadySelected) {
			CardsManager.Instance.cardsOnExecutionStack.Add (this.gameObject);
		} else {
			CardsManager.Instance.cardsOnExecutionStack.Remove (this.gameObject);
		}
		isAlreadySelected = !isAlreadySelected;
	}

	public void Execute()
	{
		switch (action) 
		{
		case Actions.Foward:
			Foward ();
			break;
		case Actions.Clockwise:
			RotateClockWise ();
			break;
		case Actions.CounterClockwise:
			RotateCounterClockWise ();
			break;
		}

	}

	public void Foward ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Forward ();
	}

	public void RotateClockWise ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Rotate(ClockRot.Clockwise);
	}

	public void RotateCounterClockWise ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Rotate(ClockRot.CounterClockwise);
	}
			
}
