using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
	private bool hasInteractions;

	private void Start()
	{
		EventManager.StartListening(EventsTypes.EnterInteractionRegion, EnteringInteractionRegionEvent);
		EventManager.StartListening(EventsTypes.ExitInteractionRegion, ExitInteractionRegionEvent);
	}

	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
	{
		if (hasInteractions && Input.GetButtonDown("Interact"))
		{
			EventManager.TriggerEvent(new OpenInteractionSelectorEvent());
		}
	}

	private void EnteringInteractionRegionEvent(EventBase @event)
	{
		var t = (EnterInteractionRegionEvent)@event;
		hasInteractions = t.Interactions.Any();
	}

	private void ExitInteractionRegionEvent(EventBase @event)
	{
		hasInteractions = false;
	}

	private void OnDestroy()
	{
		EventManager.StopListening(EventsTypes.EnterInteractionRegion, EnteringInteractionRegionEvent);
		EventManager.StopListening(EventsTypes.ExitInteractionRegion, ExitInteractionRegionEvent);
	}
}
