using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingUnit {
	public void SetCurrentTile(Tile tile){
		currentTile = tile;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.W)) {
			Forward ();
		} else if (Input.GetKeyDown (KeyCode.D)) {
			Rotate (ClockRot.Clockwise);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			Rotate (ClockRot.CounterClockwise);
		}
	}

	new void Forward(){
		base.Forward ();
	}
}
