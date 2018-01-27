using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : Singleton<MovementManager> {
	[Header("Player")]
	public Player player;

	public Tile Forward(){
		print ("Forward");

		return null;
	}

	public void Clockwise(){
		print ("Clockwise");
	}

	public void CounterClockwise(){
		print ("Counter Clockwise");
	}
}
