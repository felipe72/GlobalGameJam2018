using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogEntry{
	public DialogEntry(string _content, string _name, Sprite _avatar){
		content = _content;
		name = _name;
		avatar = _avatar;
	}

	public string content;
	public string name;
	public Sprite avatar;
}

public class DialogManager : MonoBehaviour {
	public Image bg;
	public Text content;
	public Text _name;
	public Image avatar;

	Queue<DialogEntry> queue;

	void Awake(){
		queue = new Queue<DialogEntry> ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.L)) {
			AddEntry (new DialogEntry ("Kkk eae men", "Jubileu", null));
		}
	}

	bool active;

	public void AddEntry(DialogEntry entry){
		queue.Enqueue (entry);

		if (!active) {
			TurnOn ();
		}
	}

	void TurnOn(){
		active = true;

		StartCoroutine (DisplayingText());
	}

	IEnumerator DisplayingText(){
		while (queue.Count != 0) {
			DialogEntry entry = queue.Dequeue ();
			_name.text = entry.name;
			avatar.sprite = entry.avatar;

			yield return StartCoroutine (ActivateBox());

			yield return StartCoroutine (SlowTypying(entry));

			yield return new WaitForSeconds (1);
			yield return StartCoroutine (DeactivateBox ()); 
			yield return new WaitForSeconds (2);
		}

		active = false;
	}

	IEnumerator SlowTypying(DialogEntry entry){
		foreach (var x in entry.content) {
			content.text += x;
			yield return new WaitForSeconds (.1f);
		}
	}

	IEnumerator ActivateBox(){
		content.text = "";
		var origPos = bg.rectTransform.localPosition.x;
		yield return bg.rectTransform.DOScaleX (1f, .5f).SetEase(Ease.OutBack).OnUpdate( () => {
			var pos = bg.rectTransform.localPosition;
			pos.x = origPos - (bg.rectTransform.rect.width - bg.rectTransform.rect.width*bg.rectTransform.localScale.x)/2f;
			bg.rectTransform.localPosition = pos;
		}).WaitForCompletion();
	}

	IEnumerator DeactivateBox(){
		var origPos = bg.rectTransform.localPosition;
		yield return bg.rectTransform.DOScaleX (0f, .5f).SetEase (Ease.OutBack).OnUpdate (() => {
			var pos = bg.rectTransform.localPosition;
			pos.x = origPos.x - (bg.rectTransform.rect.width - bg.rectTransform.rect.width * bg.rectTransform.localScale.x) / 2f;
			bg.rectTransform.localPosition = pos;
		}).OnComplete (() => {
			bg.rectTransform.localPosition = origPos;
		}).WaitForCompletion ();
	}


}
