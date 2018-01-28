using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public delegate void TileEvent(MovingUnit unit);

public class Tile : MonoBehaviour{
	[Header("Configurations")]
	public Vector3Int pos;
	public Sprite[] sprites;
	public GameObject danger;

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

		if (FindObjectOfType<Player> ().currentTile == this) {
			OnTileEnter (FindObjectOfType<Player> ());
		}

		danger.SetActive (true);
	}

	public void RemoveHurt(){
		onTileEnter -= HurtUnit;

		danger.SetActive (false);
	}

	public void HurtUnit(MovingUnit unit){
		if (unit.GetComponent<Player> ()) {
			CardsManager.Instance.AddCurse();
		}

		unit.GetHurt ();

		print (unit.gameObject.name + " was hurt!");
	}

	public void PushBack(MovingUnit unit){
		unit.PushBack (1);
	}
}
