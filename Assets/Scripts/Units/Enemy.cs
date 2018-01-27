using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingUnit {
	void Start(){
		SetNextTile ();
	}

	void SetNextTile(){
		int index = System.Array.FindIndex (directions, x => x == facingDirection);

		Vector3Int pos = currentTile.pos + directionsVec [index];

		Tile tile = MapManager.Instance.GetTileAt (pos);

		tile.PutHurt ();
	}
}
