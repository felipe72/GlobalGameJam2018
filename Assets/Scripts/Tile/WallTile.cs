using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallTile : Tile {
	void Awake(){
		onTileEnter += PushBack;
	}

	void PushBack(MovingUnit unit){
		unit.transform.DOShakeRotation (.3f);

		unit.PushBack (2);
	}
}
