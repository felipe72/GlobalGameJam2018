// LoadingScreenManager
// --------------------------------
// built by Martin Nerurkar (http://www.martin.nerurkar.de)
// for Nowhere Prophet (http://www.noprophet.com)
//
// Licensed under GNU General Public License v3.0
// http://www.gnu.org/licenses/gpl-3.0.txt

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingScreenManager : MonoBehaviour {
	public float minimumLoadingTime;

	[Header("Timing Settings")]
	public float waitOnLoadEnd = 0.25f;
	public float fadeDuration = 0.25f;

	[Header("Loading Settings")]
	public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
	public ThreadPriority loadThreadPriority;

	[Header("Other")]
	// If loading additive, link to the cameras audio listener, to avoid multiple active audio listeners
	public AudioListener audioListener;
	public Text dots;
	public Text loadingText;

	AsyncOperation operation;
	Scene currentScene;

	public static int sceneToLoad = -1;
	// IMPORTANT! This is the build index of your loading scene. You need to change this to match your actual scene index
	static int loadingSceneIndex = 1;

	public static void LoadScene(int levelNum) {				
		Application.backgroundLoadingPriority = ThreadPriority.High;
		sceneToLoad = levelNum;
		SceneManager.LoadScene(loadingSceneIndex);
	}

	void Start() {
		StartCoroutine (Dots());

		loadingText.rectTransform.DOScale (Vector3.one * 1.2f, .5f).SetLoops (-1, LoopType.Yoyo);

		Time.timeScale = 1f;

		if (sceneToLoad < 0) {
			return;
		}

		//fadeOverlay.gameObject.SetActive(true); // Making sure it's on so that we can crossfade Alpha
		currentScene = SceneManager.GetActiveScene();
		StartCoroutine(LoadAsync(sceneToLoad));
	}

	private IEnumerator LoadAsync(int levelNum) {
		//ShowLoadingVisuals();

		yield return null; 


		//FadeIn();
		StartOperation(levelNum);

		// operation does not auto-activate scene, so it's stuck at 0.9

		float time = minimumLoadingTime;

		while (DoneLoading() == false || time > 0) {
			time -= Time.deltaTime;

			yield return null;

		}

		if (loadSceneMode == LoadSceneMode.Additive)
			audioListener.enabled = false;


		yield return new WaitForSeconds(waitOnLoadEnd);


		yield return new WaitForSeconds(fadeDuration);

		if (loadSceneMode == LoadSceneMode.Additive)
			SceneManager.UnloadSceneAsync(currentScene.name);
		else
			operation.allowSceneActivation = true;
	}

	private void StartOperation(int levelNum) {
		Application.backgroundLoadingPriority = loadThreadPriority;
		operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);


		if (loadSceneMode == LoadSceneMode.Single)
			operation.allowSceneActivation = false;
	}

	IEnumerator Dots(){
		int num = 0;

		string s = "";

		while (true) {
			s = "";
			for (int i = 0; i < num; i++) {
				s += ". ";
			}
			dots.text = s;

			yield return new WaitForSecondsRealtime (1f);
			num = (num+1) % 4;
		}
	}

	private bool DoneLoading() {
		return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f); 
	}
}
