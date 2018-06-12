using System;
using UnityEngine;

public class TimeManager : ITimeManager
{
	public TimeSpan CurrentTime;
	private float currentTimeScale;
	private float speed;

	public TimeManager()
	{
		Time.timeScale = 1;
		speed = 1;
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
		CurrentTime = CurrentTime.Add(TimeSpan.FromSeconds(Time.deltaTime * Time.timeScale * speed));
		if (CurrentTime.Hours >= 24)
		{
			CurrentTime = CurrentTime.Subtract(TimeSpan.FromHours(24));
		}
	}

	public void SetTimeSpeed(float timeSpeed)
	{
		speed = timeSpeed;
	}
}
