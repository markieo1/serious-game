using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterInteractionRegionEvent : EventBase
{
	/// <summary>
	/// Gets or sets the possible interactions.
	/// </summary>
	public Interaction[] Interactions { get; set; }

	public override EventsTypes GetEventType()
	{
		return EventsTypes.EnterInteractionRegion;
	}
}
