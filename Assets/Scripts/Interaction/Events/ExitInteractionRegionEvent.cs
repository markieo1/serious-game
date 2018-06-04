using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitInteractionRegionEvent : EventBase
{
	public override EventsTypes GetEventType()
	{
		return EventsTypes.ExitInteractionRegion;
	}
}
