﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapManager : Singleton<MapManager> {
	[Header("Configuration")]
	public int tilesWidth;
	public int tilesHeight;
	public int numOfEnemies;

	public Vector3Int playerStartingPos;

	[Header("Game Objects")]
	public Transform tileTransform;
	public Text currentMisionText;
	public Image black;
	public Image pauseMenu;
	public SpriteRenderer arrow;

	[Header("Prefabs")]
	public GameObject tileGo;
	public GameObject tileHurtGo;
	public GameObject tileWallGo;
	public GameObject pickupGo;
	public GameObject escapePodGo;
	public Player player;
	public Enemy[] enemies;

	Tile[,] map;
	List<Enemy> enemyList;


	void Awake(){
		int currentMission = PlayerPrefs.GetInt ("currentMission", 0) + 1;
		PlayerPrefs.SetInt ("currentMission", currentMission);
		currentMisionText.text = currentMission.ToString ();

		enemyList = new List<Enemy> ();
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

		CreateWalls ();
		CreateHurtTiles ();
		CreateEnemies ();
		CreatePickups ();
		CreateEscapePod ();

		player.transform.position = playerStartingPos;
		player.SetCurrentTile (GetTileAt(playerStartingPos));
	}

	void CreateEscapePod(){
		Instantiate (escapePodGo, playerStartingPos, Quaternion.identity);
	}

	void CreatePickups(){
		Vector3Int pos = new Vector3Int (Random.Range (tilesWidth/2, tilesWidth), Random.Range (tilesHeight/2, tilesHeight), 0);

		while (GetTileAt (pos).type != TileType.Normal) {
			pos = new Vector3Int (Random.Range (tilesWidth/2, tilesWidth), Random.Range (tilesHeight/2, tilesHeight), 0);
		}

		Instantiate (pickupGo, pos, Quaternion.identity);

		arrow.transform.position = pos + Vector3.up;
		arrow.DOFade (1f, 5f).OnComplete(() =>{
			arrow.DOFade (0f, 5f);
		});
	}

	void CreateHurtTiles(){
		for (int x = 0; x < tilesWidth; x++) {
			for (int y = 0; y < tilesHeight; y++) {
				if (map [x, y].type == TileType.Normal) {
					float rng = Random.Range (0f, 1f);
					if (rng < .05f) {
						SwapTile (x, y, tileHurtGo);
					}
				}
			}
		}
	}

	void CreateEnemies(){
		bool[,] boolMap = new bool[tilesWidth, tilesHeight];

		for (int x = 0; x < tilesWidth; x++) {
			for (int y = 0; y < tilesHeight; y++) {
				boolMap [x, y] = true;

				if (map [x, y].type == TileType.Wall) {
					CornersBoolMap (x, y, boolMap);
				}
			}
		}

		boolMap [0, 0] = false;
		boolMap [0, 1] = false;
		boolMap [1, 1] = false;
		boolMap [1, 0] = false;

		string s = "";

		for (int x = 0; x < tilesWidth; x++) {
			for (int y = 0; y < tilesHeight; y++) {
				s += boolMap [x, y] ? "T" : "F";
				s += " ";
			}
			s += "\n";
		}

		for (int i = 0; i < numOfEnemies; i++) {
			Enemy enemy = enemies [Random.Range (0, enemies.Length)];
			Vector2Int pos = new Vector2Int (Random.Range (0, tilesWidth), Random.Range (0, tilesHeight));
			while (!boolMap [pos.x, pos.y]) {
				pos = new Vector2Int (Random.Range (0, tilesWidth), Random.Range (0, tilesHeight));;
			}
			CornersBoolMap (pos.x, pos.y, boolMap);
			enemyList.Add(Instantiate (enemy.gameObject, new Vector3 (pos.x, pos.y, 0), Quaternion.identity).GetComponent<Enemy>());

			if (AmountOfSpace (boolMap) == 0) {
				break;
			}
		}
	}

	int AmountOfSpace(bool[,] boolMap){
		int amount = 0;

		for (int x = 0; x < tilesWidth; x++) {
			for (int y = 0; y < tilesHeight; y++) {
				if (boolMap [x, y]) {
					amount++;
				}
			}
		}

		return amount;
	}

	void CornersBoolMap(int x, int y, bool[,] boolMap){
		UpdateBoolMap (x, y, boolMap);
		UpdateBoolMap (x, y+1, boolMap);
		UpdateBoolMap (x, y-1, boolMap);
		UpdateBoolMap (x+1, y+1, boolMap);
		UpdateBoolMap (x+1, y-1, boolMap);
		UpdateBoolMap (x+1, y, boolMap);
		UpdateBoolMap (x-1, y+1, boolMap);
		UpdateBoolMap (x-1, y-1, boolMap);
		UpdateBoolMap (x-1, y, boolMap);
	}

	void UpdateBoolMap(int x, int y, bool[,] boolMap){
		if (x >= 0 && x < tilesWidth && y >= 0 && y < tilesHeight) {
			boolMap [x, y] = false;
		}
	}

	void CreateWalls(){
		for (int i = 0; i < 2; i++) {
			GenerateWall (new Vector2Int ((6 * i + 1) % tilesWidth, Random.Range(0, 3)));
		}
	}

	WallTile GenerateWall(Vector2Int pos){
		WallTile wallTile = SwapWallTile (pos.x, pos.y);

		if (wallTile.pos.y == 0) {
			wallTile.SetSprite (WallPositions.Left);
			SwapWallTile (wallTile.pos.x + wallTile.width - 1, wallTile.pos.y).SetSprite(WallPositions.Right);
		} else {
			wallTile.SetSprite (WallPositions.BottomLeft);
			SwapWallTile (wallTile.pos.x + wallTile.width - 1, wallTile.pos.y).SetSprite(WallPositions.BottomRight);
		}

		SwapWallTile (wallTile.pos.x + wallTile.width - 1, wallTile.pos.y + wallTile.height - 1).SetSprite(WallPositions.UpperRight);
		SwapWallTile (wallTile.pos.x, wallTile.pos.y + wallTile.height - 1).SetSprite(WallPositions.UpperLeft);

		for (int i = 1; i < wallTile.width - 1; i++) {
			if (wallTile.pos.y == 0) {
				SwapWallTile (wallTile.pos.x + i, wallTile.pos.y).SetSprite (WallPositions.Center);
			} else {
				SwapWallTile (wallTile.pos.x + i, wallTile.pos.y).SetSprite (WallPositions.Bottom);
			}
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

		return wallTile;
	}

	void SwapTile(int x, int y, GameObject prefab){
		Tile tile = Instantiate (prefab, new Vector3(x, y), Quaternion.identity).GetComponent<Tile>();
		tile.pos = new Vector3Int (x, y, 0);
		tile.transform.SetParent (tileTransform);
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

	public void ArrowToEscape(){
		arrow.transform.position = FindObjectOfType<EscapePod> ().currentTile.pos + Vector3.up;
		arrow.color = Color.white;
		arrow.DOFade (1f, 5f).OnComplete(() =>{
			arrow.DOFade (0f, 5f);
		});
	}

	public bool enemyInTile(Tile tile){
		foreach (var enemy in enemyList) {
			if (enemy.currentTile == tile) {
				return true;
			}
		}

		return false;
	}

	public bool isValid(Tile tile){
		if (tile == null) {
			return false;
		}

		return true;
	}

	public void Pause(){
		Time.timeScale = 0f;
		black.raycastTarget = true;
		black.DOFade (.95f, 1f).SetUpdate(true);
		pauseMenu.rectTransform.DOLocalMoveX (pauseMenu.rectTransform.localPosition.x - 454, 1f).SetUpdate(true);
	}

	public void Resume(){
		black.DOFade (0, 1f).SetUpdate(true).OnComplete(() => {
			Time.timeScale = 1f;
			black.raycastTarget = false;
		});
		pauseMenu.rectTransform.DOLocalMoveX (pauseMenu.rectTransform.localPosition.x + 454, 1f).SetUpdate(true);
	}

	public void Exit(){
		Application.Quit ();
	}
}
