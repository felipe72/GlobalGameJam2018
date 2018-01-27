using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public delegate void TileEvent(MovingUnit unit);

public class Tile : MonoBehaviour{
	[Header("Configurations")]
	public Vector3Int pos;
	public Sprite[] sprites;

	public event TileEvent onTileEnter;
	public event TileEvent onTileExit;

	public TileType type = TileType.Normal;

	void Awake(){
		if (sprites.Length != 0) {
			GetComponent<SpriteRenderer> ().sprite = sprites [Random.Range (0, sprites.Length)];
		}
	}

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

	public void PutHurt(){
		onTileEnter += HurtUnit;

		GetComponent<SpriteRenderer> ().color = Color.red;
	}

	public void RemoveHurt(){
		onTileEnter -= HurtUnit;

		GetComponent<SpriteRenderer> ().color = Color.white;
	}

	public void HurtUnit(MovingUnit unit){
		print (unit.gameObject.name + " was hurt!");
	}

	public void PushBack(MovingUnit unit){
		unit.transform.DOShakeRotation (.3f);

		unit.PushBack (1);
	}
}
