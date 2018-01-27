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
	public Player player;

	Tile[,] map;

	void Awake(){
		GenerateMap ();	
	}

	void GenerateMap(){
		map = new Tile[tilesWidth, tilesHeight];

		for (int x = 0; x < tilesWidth; x++) {
			for (int y = 0; y < tilesHeight; y++) {
				GameObject go = Instantiate (tileGo, new Vector3 (x, y), Quaternion.identity);
				go.transform.SetParent (tileTransform);

				Tile tile = go.GetComponent<Tile> ();

				map [x, y] = tile;
			}
		}

		player.transform.position = playerStartingPos;
		player.SetCurrentTile (GetTileAt(playerStartingPos));
	}

	public Tile GetTileAt(Vector3Int pos){
		return map [pos.x, pos.y];
	}

	public bool isValid(Tile tile){
		return true;
	}
}
