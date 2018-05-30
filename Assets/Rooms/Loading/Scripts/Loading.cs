using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
	private LoadingManager loadingManager;
	private string sceneName;

	// Use this for initialization
	void Start()
	{
		loadingManager = LoadingManager.Instance;
	}

	// Update is called once per frame
	void Update()
	{
		if (loadingManager != null)
		{
			try
			{
				sceneName = loadingManager.GetNextScene();

				StartCoroutine(LoadSceneAsync(sceneName));
			}
			catch { }
		}
	}

	private IEnumerator LoadSceneAsync(string sceneName)
	{
		yield return new WaitForSeconds(1);

		AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName);

		while (!loadingScene.isDone)
		{
			yield return null;
		}
	}
}
