public class SugarLowEvent : EventBase
{
	public SugarLowEvent(float sugar, float oldSugar) : base()
	{
		this.Value = sugar;
		this.OldValue = oldSugar;
		this.Instigator = SugarLevelInstigator.UNKNOWN;
	}

	public SugarLowEvent(float sugar, float oldSugar, SugarLevelInstigator instigator) : base()
	{
		this.OldValue = oldSugar;
		this.Value = sugar;
		this.Instigator = instigator;
	}

	public float OldValue { get; private set; }

	public float Value { get; set; }

	public SugarLevelInstigator Instigator { get; set; }
}

