using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Linq;

public class CardsManager : Singleton<CardsManager> {

	public List<GameObject> cardsAvailable;

	public List<GameObject> cardsOnHand;
	public List<GameObject> cardsOnDeckStack;
	public List<GameObject> cardsOnDiscardStack;

	public List<GameObject> cardsOnExecutionStack;

	public List<GameObject> bugsCards;
	public Sprite[] icons;

	[Header("Info Variables")]
	public int actionsAmount;
	public int deckAmount = 12;

	public int specialIndex = 0;

	[Header("Canvas Positions")]
	public GameObject handCards;
	public GameObject discardCards;
	public GameObject deckCards;

	public Image[] symbolSlots;

	[Header ("Pos Stage Screen Info")]
	public Image backGround;
	public Image window;
	public List<GameObject> newCards;
	public Transform hori;



	void Awake()
	{
		if (PersistingData.Instance.cards.Count != 0) {
			cardsAvailable.Clear ();
			Transform canvas = GameObject.Find ("Canvas").GetComponent<Transform> ();
			foreach (var card in PersistingData.Instance.cards) {
				GameObject go = Instantiate (card.gameObject, deckCards.transform);
				go.transform.localPosition = Vector3.zero;
				go.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));

				cardsAvailable.Add (go);
			}

			foreach (var x in PersistingData.Instance.cards) {
				Destroy (x.gameObject);
			}
			PersistingData.Instance.cards.Clear ();
		}

		InitDeck ();

		RefreshHandCards();
		actionsAmount = 0;
	}

	void SetScreenInfo()
	{
		
	}

	void RefreshHandCards()
	{
		int currentAmount = cardsOnHand.Count;
		for (int i = 0; i < 5 - currentAmount; i++) 
		{
			if (cardsOnDeckStack.Count == 0) 
			{
				for (int j = 0; j < cardsOnDiscardStack.Count; j++) 
				{
					GameObject _card = cardsOnDiscardStack [i];
					Card cardComp = _card.GetComponent<Card> ();

					_card.transform.SetParent (deckCards.transform);
					_card.transform.localPosition = new Vector3 (0, 0, 0);
					cardsOnDeckStack.Add(_card);
					cardsOnDiscardStack.Remove (_card);
				}
				ShuffleDeck (3);
			}
				
			int cleverton = cardsOnDeckStack.Count - 1;	
			print (cardsOnDeckStack.Count.ToString () + " " + cleverton.ToString ());
			GameObject _cardjooj = cardsOnDeckStack [cleverton];
			if (_cardjooj) 
			{
				Card cardComp = _cardjooj.GetComponent<Card> ();
				_cardjooj.transform.SetParent (handCards.transform);
				cardsOnDeckStack.Remove (_cardjooj);
				cardsOnHand.Add (_cardjooj);
				cardComp.RotateToFront ();
				Debug.Log ("jooj");
				//yield return new WaitForSeconds (0.1f);
			}
		}
	}

	void ShuffleDeck(int amount)
	{
		for(int kek = 0 ; kek < amount ; kek++)
			for (int i = 0; i < cardsOnDeckStack.Count; i++) 
			{
				int rand = Random.Range (0, cardsOnDeckStack.Count);
				GameObject _aux = cardsOnDeckStack [rand];
				cardsOnDeckStack [rand] = cardsOnDeckStack [i];
				cardsOnDeckStack [i] = _aux;
			}
	}

	void InitDeck()
	{
		for (int i = 0; i < cardsAvailable.Count; i++) 
		{
			GameObject goCard =  Instantiate (cardsAvailable [i],deckCards.transform);
			cardsOnDeckStack.Add (goCard);
			goCard.transform.localPosition = new Vector3 (0, 0, 0);
		}
		ShuffleDeck (3);
	}

	public void AddToExecutionStack(Card _card)
	{
		if (!_card.isAlreadySelected) 
		{
			if (actionsAmount < 3) 
			{
				TurnManager.Instance.SetHoriGroup (false);
				cardsOnExecutionStack.Add (_card.gameObject);
				actionsAmount++;
				_card.isAlreadySelected = !_card.isAlreadySelected;
				RefreshImageSymbols ();
				_card.transform.DOKill ();
				_card.transform.DOLocalMoveY (50, 0.5f);
			}
		} else {
			cardsOnExecutionStack.Remove (_card.gameObject);
			actionsAmount--;
			_card.isAlreadySelected = !_card.isAlreadySelected;
			RefreshImageSymbols ();
			_card.transform.DOKill ();
			_card.transform.DOLocalMoveY (0, 0.5f);
		}

		TurnManager.Instance.startTurnButton.interactable = (actionsAmount >= 1);

	}

	void RefreshImageSymbols()
	{
		for (int i = 0; i < 3; i++) 
		{
			symbolSlots [i].color = Color.clear;
		}
		for (int i = 0; i < cardsOnExecutionStack.Count; i++) 
		{
			Card _card = cardsOnExecutionStack [i].GetComponent<Card> ();
			symbolSlots [i].sprite = _card.symbol;
			symbolSlots [i].color = Color.white;


			if (_card.action == Actions.JustClockWise) {
				symbolSlots [i].transform.rotation = Quaternion.Euler(new Vector3(0,0,270));
			} else if (_card.action == Actions.JustCounterClockwise) {
				symbolSlots [i].transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
			} else {
				symbolSlots [i].transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
			}

			if (_card.action == Actions.Clockwise) {
				symbolSlots [i].transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				symbolSlots [i].transform.localScale = new Vector3 (1, 1, 1);
			}
		}
	}

	public void StartTurn()
	{
		StartCoroutine (ExecuteTurn ());
	}

	IEnumerator ExecuteTurn()
	{
		TurnManager.Instance.SetHandCards (false);
		TurnManager.Instance.startTurnButton.interactable = false;
		TurnManager.Instance.SetHoriGroup (true);

		bool curse1Passive = false;
		bool curse1Active = false;

		foreach (var x in cardsOnHand.Where(x => x.GetComponent<Card>().curse1)) {
			if (cardsOnExecutionStack.Contains (x)) {
				curse1Active = true;
				break;
			} else {
				curse1Passive = true;
				break;
			}
		}

		for (int i = 0; i < cardsOnExecutionStack.Count; i++) 
		{
			GameObject _card = cardsOnExecutionStack [i];
			Card cardComp = _card.GetComponent<Card> ();

			cardsOnHand.Remove (_card);
			cardsOnDiscardStack.Add (_card);

			_card.transform.DOLocalMoveY (0, 2f);
			foreach (var x in _card.GetComponentsInChildren<Image>()) {
				x.DOFade (0f, 1.5f).OnComplete(() => {
					_card.transform.SetParent (discardCards.transform);
					_card.transform.localPosition = new Vector3 (0, 0, 0);
					cardComp.RotateToBack ();
					if(x.color.b != 0)
					x.color = Color.white;
				});
			}
		}
		float rng = Random.Range (0, 1f);
		if (rng < .5f && curse1Passive) {
			var temp = cardsOnExecutionStack [0];
			int jooj = cardsOnExecutionStack.Count;
			if (jooj == 2) {
				cardsOnExecutionStack [0] = cardsOnExecutionStack [1];
				cardsOnExecutionStack [1] = temp;
			} else if (jooj == 3) {
				cardsOnExecutionStack [0] = cardsOnExecutionStack [2];
				cardsOnExecutionStack [2] = cardsOnExecutionStack [1];
				cardsOnExecutionStack [1] = temp;
			}
			RefreshImageSymbols ();
		}

		yield return new WaitForSeconds (1f);
		for (int i = 0; i < cardsOnExecutionStack.Count; i++) 
		{
			Card _currentCard = cardsOnExecutionStack [i].GetComponent<Card> ();
			if (curse1Active) {
				rng = Random.Range (0, 1f);
				if (rng < .5f) {
					Actions[] actions = { Actions.Clockwise, Actions.CounterClockwise, Actions.Foward };
					_currentCard.action = actions [Random.Range (0, 3)];

					if (_currentCard.action == Actions.Foward) {
						_currentCard.symbol = icons [0];
					} else if (_currentCard.action == Actions.CounterClockwise) {
						_currentCard.symbol = icons [1];
					} else {
						_currentCard.symbol = icons [1];
					}
				}
			}

			RefreshImageSymbols ();
			if (_currentCard.action == Actions.TwiceNext) 
			{
				specialIndex = i;
			}
			_currentCard.Execute ();
			yield return new WaitForSeconds (1f);
		}

		cardsOnExecutionStack.Clear ();
		RefreshImageSymbols ();
		actionsAmount = 0;

		//Enemy Turn

		List<Enemy> enemies = new List<Enemy>();
		foreach (Enemy enemy in FindObjectsOfType<Enemy> ()) 
		{
			enemies.Add (enemy);
		}
		for (int j = 0; j < 3; j++) 
		{
			for (int i = 0; i < enemies.Count; i++) 
			{
				enemies [i].DoAI ();
			}
			yield return new WaitForSeconds (1f);
		}

		RefreshHandCards ();
		yield return new WaitForSeconds (0.1f);
		TurnManager.Instance.SetHandCards (true);
		TurnManager.Instance.SetAlreadyTaken (false);
	}

	public void AddCardToDeck(Card card){
		card.RotateToBack ();
		cardsOnDeckStack.Add (card.gameObject);
	}

	public void AddCurse(){
		GameObject canvas = GameObject.Find ("Canvas");
		RectTransform rect = Instantiate (bugsCards [Random.Range (0, bugsCards.Count)], canvas.transform.position, Quaternion.identity, canvas.transform).GetComponent<RectTransform>();
		rect.localPosition = new Vector3 (0, 0);

		rect.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), .5f).OnComplete (() => {
			rect.DOMove(deckCards.transform.position, .5f).OnComplete (() => {
				AddCardToDeck(rect.GetComponent<Card>());
			});
			rect.DOScale (new Vector3 (1f, 1f, 1f), .5f);
		});
	}
}
