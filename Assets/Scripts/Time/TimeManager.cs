using System;
using UnityEngine;

public class TimeManager : ITimeManager
{
	public TimeSpan CurrentTime;
	private float currentTimeScale;

	public TimeManager()
	{
		Time.timeScale = 1;
		currentTimeScale = Time.timeScale;
	}

	public TimeSpan GetTime()
	{
		return CurrentTime;
	}

	public void Pause()
	{
		Time.timeScale = 0;
	}

	public void Unpause()
	{
		Time.timeScale = currentTimeScale;
	}

	public void Tick()
	{
		CurrentTime = CurrentTime.Add(TimeSpan.FromSeconds(Time.deltaTime * Time.timeScale));
		if (CurrentTime.Hours >= 24)
		{
			CurrentTime = CurrentTime.Subtract(TimeSpan.FromHours(24));
		}
	}
}
