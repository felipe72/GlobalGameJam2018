using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
	private MapManager instance;


	void Awake(){
		GenerateMap ();	
	}

	void GenerateMap(){
		
	}

	public bool isValid(Tile tile){
		return true;
	}
}
