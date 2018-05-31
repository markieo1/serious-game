using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

	// Update is called once per frame
	void Update()
	{
		try
		{
			string sceneName = LoadingManager.Instance.GetNextScene();

			StartCoroutine(LoadSceneAsync(sceneName));
		}
		catch
		{
			Debug.LogError("Loading screen started without next scene.");
		}
	}

	private IEnumerator LoadSceneAsync(string sceneName)
	{
		AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName);

		while (!loadingScene.isDone)
		{
			yield return null;
		}
	}
}
