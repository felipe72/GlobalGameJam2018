using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MovingUnit {
	new void Start(){
		base.Start ();

		facingDirection = directions [Random.Range (0, directions.Length)];
		UpdateAnim ();
		AddPushBack ();
		SetNextTile ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.K)) {
			DoAI ();
		}
	}

	public void DoAI(){
		Tile tile = GetForwardTile ();

		if (MapManager.Instance.isValid(tile) && tile.type != TileType.Wall && !MapManager.Instance.enemyInTile(tile)) {			
			RemovePushBack ();
			MoveToTile (tile);
			AddPushBack ();
		} else {
			Rotate (ClockRot.Clockwise, 2);
		}

		if (MapManager.Instance.isValid (tile)) {
			tile.RemoveHurt ();
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

	void AddPushBack(){
		currentTile.onTileEnter += currentTile.PushBack;
	}

	void RemovePushBack(){
		currentTile.onTileEnter -= currentTile.PushBack;
	}
}
