using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EscapePod : MovingUnit {

	Pickup[] pickups;

	new void Start(){
		base.Start ();

		Initialize ();
	}

	void Initialize(){
		pickups = FindObjectsOfType<Pickup> ();

		currentTile.onTileEnter += CheckEnd;
	}

	void CheckEnd(MovingUnit unit){
		pickups = pickups.Where (x => x != null).ToArray();

		if (unit.GetComponent<Player> () != null && pickups.Length == 0) {
			LoadingScreenManager.LoadScene (0);
		}
	}

	override protected void UpdateAnim(){

	}
}
