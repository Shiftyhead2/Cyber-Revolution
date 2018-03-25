using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

	public GameObject LoadScreen;
	public GameObject MainScreen;
	public Slider LoadSlider;
	public Text ProgressText;

	public void LoadLevel(int sceneIndex){
		StartCoroutine (LoadAsynchronously (sceneIndex));


	}

	IEnumerator LoadAsynchronously(int sceneIndex){
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);

		LoadScreen.SetActive (true);
		MainScreen.SetActive (false);

		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);

			LoadSlider.value = progress;
			ProgressText.text = progress * 100f + "%";

			//Debug.Log (progress);
			yield return null;
		}
	}

}
