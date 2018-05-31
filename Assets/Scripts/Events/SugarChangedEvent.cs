public class SugarChangedEvent : EventBase
{
	public float Value { get; set; }

	public override EventsTypes GetEventType()
	{
		return EventsTypes.SugarLevelChanged;
	}
}

