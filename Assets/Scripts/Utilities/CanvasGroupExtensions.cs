using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasGroupExtensions
{
	/// <summary>
	/// Hides the specified canvas group.
	/// </summary>
	/// <param name="canvasGroup">The canvas group.</param>
	public static void Hide(this CanvasGroup canvasGroup)
	{
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
	}

	/// <summary>
	/// Shows the specified canvas group.
	/// </summary>
	/// <param name="canvasGroup">The canvas group.</param>
	public static void Show(this CanvasGroup canvasGroup)
	{
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
	}
}
