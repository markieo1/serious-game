using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
	private string sceneName;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		try
		{
			sceneName = LoadingManager.Instance.GetNextScene();

			StartCoroutine(LoadSceneAsync(sceneName));
		}
		catch
		{
			Debug.Log("Loading screen started without next scene.");
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
