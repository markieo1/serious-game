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

	public static IEnumerable<SugarLowEvent> SugarLowEvents
	{
		get
		{
			return Instance.sugarLowEvents;
		}
	}

	public static IEnumerable<SugarHighEvent> SugarHighEvents
	{
		get
		{
			return Instance.sugarHighEvents;
		}
	}

	private List<SugarChangedEvent> sugarChangeEvents;
	private List<SugarLowEvent> sugarLowEvents;
	private List<SugarHighEvent> sugarHighEvents;

	private AIManager()
	{
		this.sugarChangeEvents = new List<SugarChangedEvent>();
		this.sugarLowEvents = new List<SugarLowEvent>();
		this.sugarHighEvents = new List<SugarHighEvent>();
		EventManager.StartListening<SugarChangedEvent>(OnSugarChanged);
		EventManager.StartListening<SugarLowEvent>(OnSugarLow);
		EventManager.StartListening<SugarHighEvent>(OnSugarHigh);
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

	private void OnSugarLow(SugarLowEvent e)
	{
		this.sugarLowEvents.Add(e);
	}

	private void OnSugarHigh(SugarHighEvent e)
	{
		this.sugarHighEvents.Add(e);
	}
}
