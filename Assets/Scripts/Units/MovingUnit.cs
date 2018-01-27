using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingUnit : MonoBehaviour {
	[Header("Configuration")]
	public Direction facingDirection = Direction.North;

	protected Tile currentTile;
	protected Animator anim;

	protected Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };
	protected Vector3Int[] directionsVec = { Vector3Int.up, Vector3Int.right, Vector3Int.down, Vector3Int.left };

	void Start(){
		anim = GetComponent<Animator> ();
	}

	public void Rotate(ClockRot rot){
		int index = System.Array.FindIndex(directions, x => x == facingDirection);

		int newIndex = 0;

		if (rot == ClockRot.Clockwise) {
			newIndex = (((index + 1) % directions.Length) + directions.Length) % directions.Length;
		} else {
			newIndex = (((index - 1) % directions.Length) + directions.Length) % directions.Length;
		}

		facingDirection = directions[newIndex];

		UpdateAnim ();
	}

	protected void UpdateAnim(){
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
			return;
		}

		MoveToTile (newTile);
	}

	void MoveToTile(Tile newTile){
		currentTile.OnTileExit (this);

		currentTile = newTile;
		transform.position = currentTile.pos;

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


		Push (directions [index], length);
	}


}
