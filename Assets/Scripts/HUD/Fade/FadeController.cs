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
		// Ensure the alpha is set to 0
		FadeImage.CrossFadeAlpha(0, 0, true);
	}

	/// <summary>
	/// Fades in an image
	/// </summary>
	/// <param name="duration">The duration.</param>
	public void FadeIn(float duration = 3)
	{
		FadeImage.CrossFadeAlpha(1, 3, true);
	}

	/// <summary>
	/// Fades out an image
	/// </summary>
	/// <param name="duration">The duration.</param>
	public void FadeOut(float duration = 3)
	{
		FadeImage.CrossFadeAlpha(0, 3, true);
	}
}
