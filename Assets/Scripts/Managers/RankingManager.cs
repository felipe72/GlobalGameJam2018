using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : Singleton<RankingManager> {
	List<int> ranking;

	void Awake(){
		
		ranking = new List<int> ();

		Initialize ();
	}


	void Update(){
		if (Input.GetKeyDown (KeyCode.U)) {
			Print ();
		} else if (Input.GetKeyDown (KeyCode.I)) {
			ChangeRankings(7, "WTF");
		} else if (Input.GetKeyDown (KeyCode.O)) {
			PlayerPrefs.SetInt ("RankingInitialized", 0);
		} else if (Input.GetKeyDown (KeyCode.P)) {
			PlayerPrefs.SetInt ("currentMission", 7);
		}
	}

	void Print(){
		string s = "";
		for (int i = 9; i >= 0; i--) {
			s += i.ToString() + ": " + PlayerPrefs.GetInt(i.ToString() + "place").ToString() + " " + PlayerPrefs.GetString(i.ToString() + "name") + "\n";
		}

		print (s);
	}

	void Initialize(){
		if (PlayerPrefs.GetInt ("RankingInitialized", 0) == 0) {
			string[] names = { "AAA", "BBB", "EPP", "BRL", "USA", "HUE", "CAP", "HEY", "LIS", "TEN" };

			PlayerPrefs.SetInt ("RankingInitialized", 1);
			for (int i = 0; i < 10; i++) {
				PlayerPrefs.SetInt ((9 - i) + "place", i + 5);
				PlayerPrefs.SetString ((9 - i) + "name", names[i]);
			}

			print ("aki");
		}

		for (int i = 0; i < 10; i++) {
			ranking.Add (PlayerPrefs.GetInt (i.ToString () + "place"));
		}
	}

	public int WhichPlace(int amount){
		int place = -1;
		for (int i = 9; i >= 0; i--) {
			if (amount < ranking [i]) {
				place = i + 1;
				break;
			}
		}

		return place;
	}

	public void ChangeRankings(int amount, string _name){
		int place = WhichPlace (amount);

		if (place <= 9) {
			for (int i = 9; i > place; i--) {
				string name = PlayerPrefs.GetString ((i - 1).ToString () + "name");
				PlayerPrefs.SetString (i.ToString () + "name", name);
				ranking [i] = ranking [i - 1];
			}

			PlayerPrefs.SetString (place.ToString () + "name", _name);
			ranking [place] = amount;

			UpdateRanking ();
		}
	}

	public void UpdateRanking(){
		for (int i = 0; i < 10; i++) {
			PlayerPrefs.SetInt (i + "place", ranking[i]);
		}
	}
}
