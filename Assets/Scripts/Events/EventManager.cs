using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	private Dictionary<EventsTypes, List<Action<EventBase>>> eventDictionary;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<EventsTypes, List<Action<EventBase>>>();
		}
	}

	public static void StartListening(EventsTypes type, Action<EventBase> listener)
	{
		List<Action<EventBase>> listeners;
		if (Instance.eventDictionary.TryGetValue(type, out listeners))
		{
			listeners.Add(listener);
		}
		else
		{
			listeners = new List<Action<EventBase>>();
			listeners.Add(listener);
			Instance.eventDictionary.Add(type, listeners);
		}
	}

	public static void StopListening(EventsTypes type, Action<EventBase> listener)
	{
		List<Action<EventBase>> listeners;
		if (Instance.eventDictionary.TryGetValue(type, out listeners))
		{
			listeners.Remove(listener);
		}
	}

	public static void TriggerEvent(EventBase @event)
	{
		List<Action<EventBase>> listeners;
		if (Instance.eventDictionary.TryGetValue(@event.GetEventType(), out listeners))
		{
			listeners.ForEach(x => x.Invoke(@event));
		}
	}
}
