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

				float rng = Random.Range (0f, 1f);
				if (x == 2 && y == 2) {
					prefab = tileWallGo;
				} else if (rng < .2f) {
					prefab = tileHurtGo;
				}

				GameObject go = Instantiate (prefab, new Vector3 (x, y), Quaternion.identity);
				go.transform.SetParent (tileTransform);

				Tile tile = go.GetComponent<Tile> ();
				tile.pos = new Vector3Int (x, y, 0);

				map [x, y] = tile;
			}
		}

		player.transform.position = playerStartingPos;
		player.SetCurrentTile (GetTileAt(playerStartingPos));
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
