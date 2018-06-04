public class SugarChangedEvent : EventBase
{
	public SugarChangedEvent(float sugar) : base()
	{
		this.Value = sugar;
	}

	public float Value { get; set; }
}

