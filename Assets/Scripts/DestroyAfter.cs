using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

	public float seconds;

	void Awake () {
		Destroy (gameObject, seconds);		
	}
}
