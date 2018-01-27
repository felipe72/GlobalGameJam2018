using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TileEvent(MovingUnit unit);

public class Tile : MonoBehaviour{
	public Vector3Int pos;

	public event TileEvent onTileEnter;
	public event TileEvent onTileExit;

	public void OnTileEnter(MovingUnit unit){
		if (onTileEnter != null) {
			onTileEnter (unit);
		}
	}

	public void OnTileExit(MovingUnit unit){
		if (onTileExit != null) {
			onTileExit (unit);
		}
	}
}
