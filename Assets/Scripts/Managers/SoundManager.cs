using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class SoundInfo{
	public AudioClip clip;
	public MusicType type;
}

public class SoundManager : Singleton<SoundManager> {
	AudioSource source;

	public SoundInfo[] infos;

	void Awake(){
		if (Instance == this) {
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start(){
		source = GetComponent<AudioSource> ();
	}

	void Play(MusicType type){
		AudioClip clip = FindClip (type);

		if (source.isPlaying) {
			source.DOFade (0f, 3f).OnComplete (() => {
				source.clip = clip;
				source.Play();
				source.DOFade(1f, 3f);
			});
		} else {
			source.volume = 0f;
			source.clip = clip;
			source.Play();
			source.DOFade (1f, 3f);
		}
	}

	AudioClip FindClip(MusicType type){
		foreach (var x in infos) {
			if (x.type == type) {
				return x.clip;
			}
		}

		return null;
	}
}
