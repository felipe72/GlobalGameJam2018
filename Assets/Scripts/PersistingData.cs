using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistingData : Singleton<PersistingData> {
	public List<Card> cards;

	void Awake(){
		if (PersistingData.Instance != this) {
			Destroy (gameObject);
			return;
		}

		cards = new List<Card> ();
		DontDestroyOnLoad (gameObject);
	}
}
