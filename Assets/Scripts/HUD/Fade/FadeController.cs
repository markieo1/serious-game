using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{

	public Image FadeImage;

	// Use this for initialization
	void Start()
	{
		// Listen for the event
		EventManager.StartListening<FadeChangeEvent>(OnFadeChangeEvent);
	}

	private void OnDestroy()
	{
		EventManager.StopListening<FadeChangeEvent>(OnFadeChangeEvent);
	}

	/// <summary>
	/// Fades in an image
	/// </summary>
	/// <param name="duration">The duration.</param>
	public IEnumerator FadeIn(float duration, Color c)
	{
		float elapsedTime = 0.0f;
		FadeImage.color = c;
		while (elapsedTime < duration)
		{
			yield return null;
			elapsedTime += Time.deltaTime;
			c.a = Mathf.Clamp01(elapsedTime / duration);
			FadeImage.color = c;
		}
	}

	/// <summary>
	/// Fades out an image
	/// </summary>
	/// <param name="duration">The duration.</param>
	public IEnumerator FadeOut(float duration, Color c)
	{
		float elapsedTime = 0.0f;
		FadeImage.color = c;
		while (elapsedTime < duration)
		{
			yield return null;
			elapsedTime += Time.deltaTime;
			c.a = 1.0f - Mathf.Clamp01(elapsedTime / duration);
			FadeImage.color = c;
		}
	}

	private void OnFadeChangeEvent(FadeChangeEvent e)
	{
		StopAllCoroutines();
		switch (e.FadeType)
		{
			case FadeType.In:
				{
					StartCoroutine(FadeIn(e.Duration, e.Color));
					break;
				}
			case FadeType.Out:
				{
					StartCoroutine(FadeOut(e.Duration, e.Color));
					break;
				}
			default:
				throw new NotImplementedException();
		}
	}
}
