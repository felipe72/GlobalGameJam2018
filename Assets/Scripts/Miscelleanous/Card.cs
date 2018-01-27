using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	public string name;
	public string description;
	public int energyCost;
	public Actions action;

	public Sprite symbol;

	public bool isAlreadySelected = false;

	/*void Update()
	{
		if (Input.GetKeyDown (KeyCode.H)) 
		{
			RotateCard ();
		}
	}*/

	public void AddToExecutionStack()
	{
		CardsManager.Instance.AddToExecutionStack (this);
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

	public void RotateCard()
	{
		this.transform.DORotate (new Vector3 (0, 0, 0), 1f).SetLoops (1, LoopType.Incremental).SetEase(Ease.Linear);
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
