using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUnit : MonoBehaviour {
	[Header("Configuration")]
	public Direction facingDirection = Direction.North;

	protected Tile currentTile;

	private Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };
	private Vector2[] directionsVec = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

	public void Rotate(ClockRot rot){
		int index = System.Array.FindIndex(directions, x => x == facingDirection);

		int newIndex = 0;

		if (rot == ClockRot.Clockwise) {
			transform.Rotate (new Vector3 (0, 0, -90));
			newIndex = (((index + 1) % directions.Length) + directions.Length) % directions.Length;
		} else {
			transform.Rotate (new Vector3 (0, 0, 90));
			newIndex = (((index - 1) % directions.Length) + directions.Length) % directions.Length;
		}

		facingDirection = directions[newIndex];
	}


}
