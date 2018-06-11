public class SugarChangedEvent : EventBase
{
	public SugarChangedEvent(float sugar) : base()
	{
		this.Value = sugar;
	}

	public SugarChangedEvent(float sugar, SugarLevelInstigator instigator) : base()
	{
		this.Value = sugar;
		this.Instigator = instigator;
	}

	public float Value { get; set; }

	public SugarLevelInstigator Instigator { get; set; }
}

