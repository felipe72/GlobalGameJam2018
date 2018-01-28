using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Nuvem : MonoBehaviour {
	public void CanMove(){
		if (transform.position.x < -10) {
			transform.position = new Vector3 (20, Random.Range (0f, 4f), 0);
		}

		transform.DOLocalMoveX (-6, Random.Range(50f, 70f));
	}

	public void StopMoving(){
		transform.DOKill ();
	}
}
