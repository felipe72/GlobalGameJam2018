using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtTile : Tile {
	void Awake(){
		onTileEnter += HurtUnit;
	}

	public void HurtUnit(MovingUnit unit){
		print (unit.gameObject.name + " was hurt!");
	}
}
