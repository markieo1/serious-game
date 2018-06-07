public class InteractionSelectorChangeEvent : EventBase
{
	/// <summary>
	/// Gets or sets a value indicating whether the interaction should open
	/// </summary>
	public bool ShouldOpen { get; set; }

	public InteractionSelectorChangeEvent(bool shouldOpen)
	{
		ShouldOpen = shouldOpen;
	}
}