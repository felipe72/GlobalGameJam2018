using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallTile : Tile {
	[Range(2, 5)]
	public int height;
	[Range(2, 5)]
	public int width;

	public Sprite[] bottomRight;
	public Sprite[] bottom;
	public Sprite[] bottomLeft;
	public Sprite[] left;
	public Sprite[] upperLeft;
	public Sprite[] upper;
	public Sprite[] upperRight;
	public Sprite[] right;
	public Sprite[] center;

	void Awake(){
		type = TileType.Wall;

		height = Random.Range (2, 6);
		width = Random.Range (2, 6);

		height = Mathf.Max (2, height);
		width = Mathf.Max (2, width);

		height = Mathf.Min (5, height);
		width = Mathf.Min (5, width);

		onTileEnter += PushBack;
	}

	public void SetSprite(WallPositions position){
		Sprite sprite = null;

		switch (position) {
		case WallPositions.BottomRight:
			sprite = Rand (bottomRight);
			break;
		case WallPositions.Bottom:
			sprite = Rand (bottom);
			break;
		case WallPositions.BottomLeft:
			sprite = Rand (bottomLeft);
			break;
		case WallPositions.Left:
			sprite = Rand (left);
			break;
		case WallPositions.UpperLeft:
			sprite = Rand (upperLeft);
			break;
		case WallPositions.Upper:
			sprite = Rand (upper);
			break;
		case WallPositions.UpperRight:
			sprite = Rand (upperRight);
			break;
		case WallPositions.Right:
			sprite = Rand (right);
			transform.localScale = new Vector3 (-1, 1, 1);
			break;
		case WallPositions.Center:
			sprite = Rand (center);
			break;
		}

		GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	T Rand<T>(T[] array){
		return array [Random.Range (0, array.Length)];
	}
}
