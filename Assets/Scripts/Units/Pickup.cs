using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MovingUnit {
	new void Start(){
		base.Start ();

		Initialize ();
	}

	void Initialize(){
		currentTile.onTileEnter += CatchPickup;
	}

	void CatchPickup(MovingUnit unit){
		if (unit.GetComponent<Player> () != null) {
			Destroy (gameObject);
		}
	}

	override protected void UpdateAnim(){

	}
}
