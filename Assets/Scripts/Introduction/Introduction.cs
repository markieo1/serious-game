using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
	[Scene]
	public string LoadingScene;

	[Scene]
	public string NewScene;

	public float Duration;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(StartCoroutineChain());
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.anyKey)
		{
			StartCoroutine(StartCoroutineSkipChain());
		}
	}

	private IEnumerator StartCoroutineChain()
	{
		yield return StartCoroutine(FadeOut());

		yield return StartCoroutine(WaitFor(15));

		yield return StartCoroutine(FadeIn());

		yield return StartCoroutine(WaitFor(3));

		yield return StartCoroutine(LoadNewScene());
	}

	private IEnumerator StartCoroutineSkipChain()
	{
		yield return StartCoroutine(FadeIn());

		yield return StartCoroutine(WaitFor(3));

		yield return StartCoroutine(LoadNewScene());
	}

	private IEnumerator WaitFor(float duration)
	{
		yield return new WaitForSeconds(duration);
	}

	private IEnumerator FadeOut()
	{
		EventManager.TriggerEvent(new FadeChangeEvent(FadeType.Out, Duration));

		yield return null;
	}

	private IEnumerator FadeIn()
	{
		EventManager.TriggerEvent(new FadeChangeEvent(FadeType.In, Duration));

		yield return null;
	}

	private IEnumerator LoadNewScene()
	{
		LoadingManager.Instance.SetSceneToLoad(NewScene);

		SceneManager.LoadScene(LoadingScene, LoadSceneMode.Single);

		yield return null;
	}
}
