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
	public Text costText;

	public bool curse1;

	public Sprite symbol;

	public bool isAlreadySelected = false;

	void Start(){
		costText.text = energyCost.ToString ();
	}

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
		EnergyManager.Instance.ChangeEnergy (-energyCost);
		print (-energyCost);
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
		case Actions.Recharge:
			RechargeLife ();
			break;
		case Actions.JustClockWise:
			JustRotateClockWise ();
			break;
		case Actions.JustCounterClockwise:
			JustRotateCounterClockWise ();
			break;
		case Actions.TurnBack:
			TurnBack ();
			break;
		case Actions.Dash:
			Dash ();
			break;
		case Actions.TwiceNext:
			TwiceNext ();
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

	public void RechargeLife ()
	{
		StartCoroutine (ActionRechargeLife ());
	}

	IEnumerator ActionRechargeLife ()
	{
		yield return new WaitForSeconds (0.4f);
		EnergyManager.Instance.ChangeEnergy (3);
		yield return new WaitForSeconds (0.4f);
	}

	public void JustRotateClockWise ()
	{
		StartCoroutine (ActionJustRotateClockWise ());
	}

	IEnumerator ActionJustRotateClockWise ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Rotate(ClockRot.Clockwise);
		yield return new WaitForSeconds (0.8f);
	}

	public void JustRotateCounterClockWise ()
	{
		StartCoroutine (ActionJustRotateCounterClockWise ());
	}

	IEnumerator ActionJustRotateCounterClockWise ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Rotate(ClockRot.CounterClockwise);
		yield return new WaitForSeconds (0.8f);
	}

	public void TurnBack ()
	{
		StartCoroutine (ActionTurnBack ());
	}

	IEnumerator ActionTurnBack ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Forward ();
		yield return new WaitForSeconds (0.3f);
		go.Rotate(ClockRot.CounterClockwise);
		yield return new WaitForSeconds (0.1f);
		go.Rotate(ClockRot.CounterClockwise);
		yield return new WaitForSeconds (0.8f);
	}

	public void Dash ()
	{
		StartCoroutine (ActionDash ());
	}

	IEnumerator ActionDash ()
	{
		Player go = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		go.Forward ();
		yield return new WaitForSeconds (0.5f);
		go.Forward ();
		yield return new WaitForSeconds (0.5f);
		go.Forward ();
		yield return new WaitForSeconds (0.8f);
	}

	public void TwiceNext ()
	{
		StartCoroutine (ActionTwiceNext ());
	}

	IEnumerator ActionTwiceNext ()
	{
		int _count = CardsManager.Instance.cardsOnExecutionStack.Count;
		int _pos = CardsManager.Instance.specialIndex + 1;
		if (_count != _pos) 
		{
			GameObject _go = CardsManager.Instance.cardsOnExecutionStack [_pos];
			Card _card = _go.GetComponent<Card> ();
			_card.Execute ();
		}
		yield return new WaitForSeconds (0.1f);
	}


			
}
