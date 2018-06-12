using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AIManager
{
	private static AIManager mInstance;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	public static AIManager Instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = new AIManager();
			}

			return mInstance;
		}
	}

	public static IEnumerable<SugarChangedEvent> SugarEvents
	{
		get
		{
			return Instance.sugarChangeEvents;
		}
	}

	private List<SugarChangedEvent> sugarChangeEvents;

	private AIManager()
	{
		this.sugarChangeEvents = new List<SugarChangedEvent>();
		EventManager.StartListening<SugarChangedEvent>(OnSugarChanged);
	}

	private void OnSugarChanged(SugarChangedEvent e)
	{
		// Don't fill up the list with natural decay events
		if (e.Instigator == SugarLevelInstigator.DECAY)
		{
			return;
		}

		this.sugarChangeEvents.Add(e);
	}
}
