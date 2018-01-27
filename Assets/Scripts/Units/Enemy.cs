using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingUnit {
	new void Start(){
		base.Start ();

		facingDirection = directions [Random.Range (0, directions.Length)];
		UpdateAnim ();

		SetNextTile ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.K)) {
			DoAI ();
		}
	}

	void DoAI(){
		Tile tile = GetForwardTile ();

		if (MapManager.Instance.isValid(tile) && tile.type != TileType.Wall) {
			tile.RemoveHurt ();
			MoveToTile (tile);
		} else {
			Rotate (ClockRot.Clockwise, 2);
		}

		SetNextTile ();
	}

	void SetNextTile(){
		Tile tile = GetForwardTile ();
		if (MapManager.Instance.isValid(tile) && tile.type != TileType.Wall) {
			tile.PutHurt ();
		}
	}

	Tile GetForwardTile(){
		int index = System.Array.FindIndex (directions, x => x == facingDirection);

		Vector3Int pos = currentTile.pos + directionsVec [index];

		Tile tile = MapManager.Instance.GetTileAt (pos);

		return tile;
	}
}
