using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseChangeEvent : EventBase
{
	/// <summary>
	/// Gets or sets a value indicating whether the game is paused.
	/// </summary>
	public bool IsPaused { get; set; }

	public GamePauseChangeEvent(bool isPaused)
	{
		IsPaused = isPaused;
	}
}