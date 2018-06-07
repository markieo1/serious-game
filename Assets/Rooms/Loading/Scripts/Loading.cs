using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
	public Fade Fading;

	void Start()
	{
		string sceneName = LoadingManager.Instance.GetNextScene();

		StartCoroutine(LoadSceneAsync(sceneName));
	}

	private IEnumerator LoadSceneAsync(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			Debug.LogError("Loading screen started without next scene.");
			yield break;
		}

		yield return StartCoroutine(Fading.FadeIn());

		yield return StartCoroutine(Fading.FadeOut());

		AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName);

		while (!loadingScene.isDone)
		{
			yield return null;
		}
	}
}
