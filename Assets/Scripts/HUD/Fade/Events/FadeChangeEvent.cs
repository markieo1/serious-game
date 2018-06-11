using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeChangeEvent : EventBase
{
	/// <summary>
	/// Gets or sets the type of the fade.
	/// </summary>
	public FadeType FadeType { get; set; }

	/// <summary>
	/// Gets or sets the duration of the fade.
	/// </summary>
	public float Duration { get; set; }

	public FadeChangeEvent(FadeType type, float duration = 3)
	{
		FadeType = type;
		Duration = duration;
	}
}