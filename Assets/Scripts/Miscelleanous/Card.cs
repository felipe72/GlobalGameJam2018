using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	public bool chooseCard;

	public string name;
	public string description;
	public int energyCost;
	public Actions action;
	public Sprite sprite;

	public bool curse1;

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

	public void RotateToFront()
	{
		this.transform.DORotate (new Vector3 (0, 0, 0), 1f).SetLoops (1, LoopType.Incremental).SetEase(Ease.Linear);
	}

	public void RotateToBack()
	{
		this.transform.DORotate (new Vector3 (0, 180, 0), 1f).SetLoops (1, LoopType.Incremental).SetEase (Ease.Linear);
	}

	public void Foward ()
	{
		StartCoroutine (ActionFoward ());
	}

	IEnumerator ActionFoward()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Forward ();
		yield return null;
	}

	public void RotateClockWise ()
	{
		StartCoroutine (ActionRotateClockWise ());
	}

	IEnumerator ActionRotateClockWise ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Forward ();
		yield return new WaitForSeconds (0.8f);
		go.Rotate(ClockRot.Clockwise);
		yield return new WaitForSeconds (0.2f);
	}

	public void RotateCounterClockWise ()
	{
		StartCoroutine (ActionRotateCounterClockWise ());
	}

	IEnumerator ActionRotateCounterClockWise ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Forward ();
		yield return new WaitForSeconds (0.8f);
		go.Rotate(ClockRot.CounterClockwise);
		yield return new WaitForSeconds (0.2f);
	}


			
}
