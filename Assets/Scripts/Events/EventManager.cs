using System;
using System.Collections.Generic;
using UnityEngine;

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

	public delegate void EventDelegate<T>(T e) where T : EventBase;


	private Dictionary<Type, Delegate> delegates;

	private EventManager()
	{
		delegates = new Dictionary<Type, Delegate>();
	}

	/// <summary>
	/// Starts the listening.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="listener">The listener.</param>
	public static void StartListening<T>(EventDelegate<T> del) where T : EventBase
	{
		if (Instance.delegates.ContainsKey(typeof(T)))
		{
			System.Delegate tempDel = Instance.delegates[typeof(T)];

			Instance.delegates[typeof(T)] = System.Delegate.Combine(tempDel, del);
		}
		else
		{
			Instance.delegates[typeof(T)] = del;
		}
	}

	/// <summary>
	/// Stops the listening.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="listener">The listener.</param>
	public static void StopListening<T>(EventDelegate<T> del) where T : EventBase
	{
		if (Instance.delegates.ContainsKey(typeof(T)))
		{
			var currentDel = System.Delegate.Remove(Instance.delegates[typeof(T)], del);

			if (currentDel == null)
			{
				Instance.delegates.Remove(typeof(T));
			}
			else
			{
				Instance.delegates[typeof(T)] = currentDel;
			}
		}
	}

	/// <summary>
	/// Triggers the event.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="event">The event.</param>
	public static void TriggerEvent<T>(T @event) where T : EventBase
	{
		if (@event == null)
		{
			Debug.LogError("Invalid event argument: " + @event.GetType().ToString());
			return;
		}

		if (Instance.delegates.ContainsKey(@event.GetType()))
		{
			Instance.delegates[@event.GetType()].DynamicInvoke(@event);
		}
	}
}
