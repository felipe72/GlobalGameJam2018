using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager> {
	[Header("Configuration")]
	public int tilesWidth;
	public int tilesHeight;

	public Vector3Int playerStartingPos;

	[Header("Game Objects")]
	public Transform tileTransform;

	[Header("Prefabs")]
	public GameObject tileGo;
	public GameObject tileHurtGo;
	public GameObject tileWallGo;
	public Player player;

	Tile[,] map;

	void Awake(){
		GenerateMap ();	
	}

	void GenerateMap(){
		map = new Tile[tilesWidth, tilesHeight];

		for (int x = 0; x < tilesWidth; x++) {
			for (int y = 0; y < tilesHeight; y++) {
				GameObject prefab = tileGo;
				/*
				float rng = Random.Range (0f, 1f);
				if (x == 2 && y == 2) {
					prefab = tileWallGo;
				} else if (rng < .1f) {
					// prefab = tileHurtGo;
				}*/

				SwapTile (x, y, prefab);
			}
		}

		GenerateWalls ();

		player.transform.position = playerStartingPos;
		player.SetCurrentTile (GetTileAt(playerStartingPos));
	}

	void GenerateWalls(){
		WallTile wallTile = SwapWallTile (3, 3);
		wallTile.SetSprite (WallPositions.BottomLeft);
		SwapWallTile (wallTile.pos.x + wallTile.width - 1, wallTile.pos.y).SetSprite(WallPositions.BottomRight);
		SwapWallTile (wallTile.pos.x + wallTile.width - 1, wallTile.pos.y + wallTile.height - 1).SetSprite(WallPositions.UpperRight);
		SwapWallTile (wallTile.pos.x, wallTile.pos.y + wallTile.height - 1).SetSprite(WallPositions.UpperLeft);

		for (int i = 1; i < wallTile.width - 1; i++) {
			SwapWallTile (wallTile.pos.x + i, wallTile.pos.y).SetSprite(WallPositions.Bottom);
			SwapWallTile (wallTile.pos.x + i, wallTile.pos.y + wallTile.height - 1).SetSprite(WallPositions.Upper);
		}

		for (int i = 1; i < wallTile.height - 1; i++) {
			SwapWallTile (wallTile.pos.x, wallTile.pos.y + i).SetSprite(WallPositions.Left);
			SwapWallTile (wallTile.pos.x + wallTile.width - 1, wallTile.pos.y + i).SetSprite(WallPositions.Right);
		}

		for (int x = 1; x < wallTile.width - 1; x++) {
			for (int y = 1; y < wallTile.height - 1; y++) {
				SwapWallTile (x + wallTile.pos.x, y + wallTile.pos.y).SetSprite(WallPositions.Center);
			}
		}



		/*
		for (int x = 0; x < wallTile.width; x++) {
			for (int y = 0; y < wallTile.height; y++) {
				if (x != 0 || y != 0) {
					SwapTile (x + wallTile.pos.x, y + wallTile.pos.y, tileWallGo);
				}
			}
		}*/
	}

	void SwapTile(int x, int y, GameObject prefab){
		Tile tile = Instantiate (prefab, new Vector3(x, y), Quaternion.identity).GetComponent<Tile>();
		tile.pos = new Vector3Int (x, y, 0);
		if (map [x, y] != null) {
			Destroy (map [x, y].gameObject);
		}
		map [x, y] = tile;
	}

	WallTile SwapWallTile(int x, int y){
		Vector2Int pos = new Vector2Int (x, y);

		WallTile wallTile = Instantiate (tileWallGo, new Vector3(pos.x, pos.y), Quaternion.identity).GetComponent<WallTile>();
		wallTile.pos = new Vector3Int (pos.x, pos.y, 0);
		if (map [pos.x, pos.y] != null) {
			Destroy (map [pos.x, pos.y].gameObject);
		}
		map [pos.x, pos.y] = wallTile;

		return wallTile;
	}

	public Tile GetTileAt(Vector3Int pos){
		if(pos.x >= tilesWidth || pos.x < 0 || pos.y >= tilesHeight || pos.y < 0){
			return null;
		}

		return map [pos.x, pos.y];
	}

	public bool isValid(Tile tile){
		if (tile == null) {
			return false;
		}

		return true;
	}
}
