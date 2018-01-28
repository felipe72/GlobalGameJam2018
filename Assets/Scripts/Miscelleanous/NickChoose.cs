using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickChoose : MonoBehaviour {
	public Text text;

	string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	void Start(){
		text = GetComponent<Text> ();
		text.text = "A";
	}

	public void Up(){
		char c = text.text [0];

		int index = alphabet.IndexOf (c);
		index = (index + 1) % alphabet.Length;
		text.text = alphabet [index].ToString ();
	}

	public void Down(){
		char c = text.text [0];

		int index = alphabet.IndexOf (c);
		index = (((index - 1) % alphabet.Length) + alphabet.Length) % alphabet.Length;
		text.text = alphabet [index].ToString ();
	}
}
