using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ITimeManager
{
	/// <summary>
	/// Pauses the game.
	/// </summary>
	void Pause();

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	void Unpause();

	/// <summary>
	/// Gets the time.
	/// </summary>
	/// <returns></returns>
	TimeSpan GetTime();

	/// <summary>
	/// Ticks.
	/// </summary>
	void Tick();

	/// <summary>
	/// Sets the time scale.
	/// </summary>
	/// <param name="timeScale">The time scale.</param>
	void SetTimeScale(float timeScale);

	/// <summary>
	/// Determines whether it is day.
	/// </summary>
	bool IsDay();
}
