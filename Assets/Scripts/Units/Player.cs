using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MovingUnit {
	public GameObject[] hits;
	public Image bg;
	public Image black;
	public Text texts;
	public Text[] rankTexts;
	public GameObject changeRanking;

	public void SetCurrentTile(Tile tile){
		currentTile = tile;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.W)) {
			Forward ();
		} else if (Input.GetKeyDown (KeyCode.D)) {
			Rotate (ClockRot.Clockwise);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			Rotate (ClockRot.CounterClockwise);
		} else if (Input.GetKeyDown (KeyCode.X)) {
			Die ();
		}
	}

	public override void GetHurt (){
		base.GetHurt ();

		GameObject go = Instantiate (hits [Random.Range (0, hits.Length)], transform);
		go.transform.localPosition = Vector3.zero;
	}

	public void Die(){
		StartCoroutine (die ());
	}

	IEnumerator die(){
		black.raycastTarget = true;

		foreach (var x in GetComponentsInChildren<Animator>()) {
			x.SetTrigger ("loop");
			yield return new WaitForSeconds (Random.Range (0.8f, 1.2f));
		}

		yield return new WaitForSeconds (3f);

		black.DOFade (.95f, 1f);
		bg.rectTransform.DOLocalMoveY (0, 1f);

		texts.text = PlayerPrefs.GetInt ("currentMission").ToString ();
		print (RankingManager.Instance.WhichPlace (PlayerPrefs.GetInt ("currentMission")));
		if (RankingManager.Instance.WhichPlace (PlayerPrefs.GetInt ("currentMission")) <= 9) {
			changeRanking.SetActive (true);
		}
	}

	public void ReturnToMenu(){
		if (RankingManager.Instance.WhichPlace (PlayerPrefs.GetInt ("currentMission")) <= 9) {
			string s = "";
			foreach (var text in rankTexts) {
				s += text.text;
			}
			print ("ola");
			RankingManager.Instance.ChangeRankings (PlayerPrefs.GetInt ("currentMission"), s);
		}

		LoadingScreenManager.LoadScene (0);
	}
}
