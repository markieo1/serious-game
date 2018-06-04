using System;
using System.Collections.Generic;

public class EventManager
{
	private static EventManager mInstance;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	public static EventManager Instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = new EventManager();
			}

			return mInstance;
		}
	}

	private Dictionary<Type, List<Action<EventBase>>> eventDictionary;

	private EventManager()
	{
		eventDictionary = new Dictionary<Type, List<Action<EventBase>>>();
	}

	/// <summary>
	/// Starts the listening.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="listener">The listener.</param>
	public static void StartListening<T>(Action<T> listener) where T : EventBase
	{
		List<Action<EventBase>> listeners;
		Action<EventBase> internalListener = (e) => listener((T)e);

		if (Instance.eventDictionary.TryGetValue(typeof(T), out listeners))
		{
			listeners.Add(internalListener);
		}
		else
		{
			listeners = new List<Action<EventBase>>();
			listeners.Add(internalListener);
			Instance.eventDictionary.Add(typeof(T), listeners);
		}
	}

	/// <summary>
	/// Stops the listening.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="listener">The listener.</param>
	public static void StopListening<T>(Action<T> listener) where T : EventBase
	{
		List<Action<EventBase>> listeners;
		Action<EventBase> internalListener = (e) => listener((T)e);

		if (Instance.eventDictionary.TryGetValue(typeof(T), out listeners))
		{
			listeners.Remove(internalListener);
		}
	}

	/// <summary>
	/// Triggers the event.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="event">The event.</param>
	public static void TriggerEvent<T>(T @event) where T : EventBase
	{
		List<Action<EventBase>> listeners;
		if (Instance.eventDictionary.TryGetValue(typeof(T), out listeners))
		{
			listeners.ForEach(x => x.Invoke(@event));
		}
	}
}
