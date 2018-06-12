using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ITimeManager
{
	void Pause();

	void Unpause();

	TimeSpan GetTime();

	void Tick();

	void SetTimeSpeed(float timeSpeed);
}
