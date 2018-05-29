using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	public Text LoadingText;
	public int Scene;

	private bool loading = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Check whether the player has pressed space bar and whether "loading" is false
		// Set "loading" to true, set the "LoadingText" and start a new scene
		if (Input.GetKeyUp(KeyCode.Space) && loading == false)
		{
			loading = true;
			LoadingText.text = "Loading...";

			StartCoroutine(LoadNewScene());
		}

		// Flashes the "LoadingText" when "loading" is true
		if (loading == true)
		{
			LoadingText.color = new Color(
				LoadingText.color.r,
				LoadingText.color.b,
				Mathf.PingPong(Time.time, 1)
			);
		}
	}

	private IEnumerator LoadNewScene()
	{
		yield return new WaitForSeconds(3);

		AsyncOperation loadingScene = SceneManager.LoadSceneAsync(Scene);

		while (!loadingScene.isDone)
		{
			yield return null;
		}
	}
}
