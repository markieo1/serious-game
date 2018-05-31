using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Events;

public class EventManager : MonoBehaviour {

	private static EventManager eventManager;

	private Dictionary<string, EventBase> eventDictionary;

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
			eventDictionary = new Dictionary<string, EventBase>();
		}
	}

	public static void StartListening(string eventName, UnityAction<Hashtable> listener)
	{
		EventBase thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new EventBase();
			thisEvent.AddListener(listener);
			instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction<Hashtable> listener)
	{
		if (eventManager == null) return;
		EventBase thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	public static void TriggerEvent(string eventName, Hashtable eventParams = default(Hashtable))
	{
		EventBase thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke(eventParams);
		}
	}

	public static void TriggerEvent(string eventName)
	{
		TriggerEvent(eventName, null);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
