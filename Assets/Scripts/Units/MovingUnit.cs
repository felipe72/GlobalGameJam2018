using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingUnit : MonoBehaviour {
	[Header("Configuration")]
	public Direction facingDirection = Direction.North;

	[HideInInspector]
	public Tile currentTile;
	protected Animator anim;

	protected Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };
	protected Vector3Int[] directionsVec = { Vector3Int.up, Vector3Int.right, Vector3Int.down, Vector3Int.left };

	protected void Start(){
		anim = GetComponent<Animator> ();

		UpdateAnim ();

		currentTile = MapManager.Instance.GetTileAt (new Vector3Int((int) transform.position.x, (int) transform.position.y, 0));
	}

	public void Rotate(ClockRot rot, int amount=1){
		int index = System.Array.FindIndex(directions, x => x == facingDirection);

		int newIndex = 0;
		if (rot == ClockRot.Clockwise) {
			newIndex = (((index + amount) % directions.Length) + directions.Length) % directions.Length;
		} else {
			newIndex = (((index - amount) % directions.Length) + directions.Length) % directions.Length;
		}

		facingDirection = directions[newIndex];

		UpdateAnim ();
	}

	virtual protected void UpdateAnim(){
		switch (facingDirection) {
		case Direction.East:
			anim.SetTrigger ("right");	
			break;
		case Direction.West:
			anim.SetTrigger ("left");	
			break;
		case Direction.North:
			anim.SetTrigger ("up");	
			break;
		case Direction.South:
			anim.SetTrigger ("down");	
			break;
		}
	}

	public void Forward(int lenght = 1){
		int index = System.Array.FindIndex(directions, x => x == facingDirection);

		Tile newTile = MapManager.Instance.GetTileAt(currentTile.pos + directionsVec[index]);

		if(!MapManager.Instance.isValid(newTile)){
			PushBack (0);
			return;
		}

		MoveToTile (newTile);
	}

	protected void MoveToTile(Tile newTile){
		currentTile.OnTileExit (this);

		currentTile = newTile;
		transform.DOMove(currentTile.pos, 1f).SetEase(Ease.InOutQuad);

		newTile.OnTileEnter (this);
	}

	public void Push(Direction direction, int length){
		int index = System.Array.FindIndex(directions, x => x == direction);
		Tile newTile = MapManager.Instance.GetTileAt(currentTile.pos + directionsVec[index] * length);

		MoveToTile (newTile);
	}

	public void PushBack(int length){
		int index = System.Array.FindIndex(directions, x => x == facingDirection);

		index = (index + 2) % directions.Length;
		transform.DOShakeRotation (.3f);


		Push (directions [index], length);
	}


}
