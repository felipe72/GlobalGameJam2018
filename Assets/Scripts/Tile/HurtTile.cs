using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtTile : Tile {
	void Awake(){
		type = TileType.Hurt;
		onTileEnter += HurtUnit;

		if (sprites.Length != 0) {
			GetComponent<SpriteRenderer> ().sprite = sprites [Random.Range (0, sprites.Length)];
		}
	}
}
