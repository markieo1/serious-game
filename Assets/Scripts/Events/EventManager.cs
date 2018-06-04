﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	private Dictionary<Type, List<Action<EventBase>>> eventDictionary;

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
			eventDictionary = new Dictionary<Type, List<Action<EventBase>>>();
		}
	}

	private void OnDestroy()
	{
		// Clear the event dictionary
		eventDictionary.Clear();
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
