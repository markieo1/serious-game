using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupItem
{
	/// <summary>
	/// The delay in seconds
	/// </summary>
	public float DelayInSeconds = 0;

	/// <summary>
	/// The display time in seconds
	/// </summary>
	public float DisplayTimeInSeconds = 0;

	/// <summary>
	/// The text
	/// </summary>
	public string Text;

	/// <summary>
	/// Initializes a new instance of the <see cref=".PopupItem"/> class.
	/// </summary>
	/// <param name="text">The text.</param>
	protected PopupItem(string text)
	{
		Text = text;
	}

	protected PopupItem(string text, float delay) : this(text)
	{
		DelayInSeconds = delay;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref=".PopupItem"/> class.
	/// </summary>
	/// <param name="text">The text.</param>
	/// <param name="delay">The delay.</param>
	/// <param name="display">The display.</param>
	protected PopupItem(string text, float delay, float display) : this(text, delay)
	{
		DisplayTimeInSeconds = display;
	}

	/// <summary>
	/// Creates an popupitem that is displayed indefinitely.
	/// </summary>
	/// <param name="text">The text.</param>
	/// <returns></returns>
	public static PopupItem Indefinitely(string text)
	{
		return new PopupItem(text);
	}

	/// <summary>
	/// Withes the delay.
	/// </summary>
	/// <param name="text">The text.</param>
	/// <param name="delay">The delay.</param>
	/// <returns></returns>
	public static PopupItem WithDelay(string text, float delay)
	{
		return new PopupItem(text, delay);
	}

	/// <summary>
	/// Withes the delay and limit.
	/// </summary>
	/// <param name="text">The text.</param>
	/// <param name="delay">The delay.</param>
	/// <param name="display">The display.</param>
	/// <returns></returns>
	public static PopupItem WithDelayAndLimit(string text, float delay, float display)
	{
		return new PopupItem(text, delay, display);
	}
}
