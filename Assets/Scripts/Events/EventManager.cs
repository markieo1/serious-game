using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

	private static EventManager eventManager;

	private Dictionary<EventsTypes, List<Action<EventBase>>> eventDictionary;

	public static EventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

				if (!eventManager)
				{
					Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init();
				}
			}

			return eventManager;
		}
	}

	void Init()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<EventsTypes, List<Action<EventBase>>>();
		}
	}

	public static void StartListening(EventsTypes type, Action<EventBase> listener)
	{
		List<Action<EventBase>> listeners;
		if (instance.eventDictionary.TryGetValue(type, out listeners))
		{
			listeners.Add(listener);
		}
		else
		{
			listeners = new List<Action<EventBase>>();
			listeners.Add(listener);
			instance.eventDictionary.Add(type, listeners);
		}
	}

	public static void StopListening(EventsTypes type, Action<EventBase> listener)
	{
		List<Action<EventBase>> listeners;
		if (instance.eventDictionary.TryGetValue(type, out listeners))
		{
			listeners.Remove(listener);
		}
	}

	public static void TriggerEvent(EventBase @event)
	{
		List<Action<EventBase>> listeners;
		if (instance.eventDictionary.TryGetValue(@event.GetEventType(), out listeners))
		{
			listeners.ForEach(x => x.Invoke(@event));
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
