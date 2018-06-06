using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Popup")]
public class PopupReaction : Reaction
{
	/// <summary>
	/// The delay in seconds
	/// </summary>
	public float DelayInSeconds = 0;

	/// <summary>
	/// The display time in seconds
	/// </summary>
	public float DisplayTimeInSeconds = 0;

	/// <summary>
	/// The text
	/// </summary>
	public string Text;

	public override void React()
	{
		EventManager.TriggerEvent(new ShowPopupEvent(PopupItem.WithDelayAndLimit(Text, DelayInSeconds, DisplayTimeInSeconds)));
	}
}
