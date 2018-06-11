using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Fade")]
public class FadeReaction : Reaction
{
	/// <summary>
	/// The fade type
	/// </summary>
	public FadeType Type;

	/// <summary>
	/// The duration
	/// </summary>
	public float Duration = 3;

	public override void React()
	{
		EventManager.TriggerEvent(new FadeChangeEvent(Type, Duration));
	}
}
