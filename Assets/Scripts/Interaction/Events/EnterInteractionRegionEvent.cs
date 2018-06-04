using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterInteractionRegionEvent : EventBase
{
	/// <summary>
	/// Gets or sets the possible interactions.
	/// </summary>
	public Interaction[] Interactions { get; protected set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="EnterInteractionRegionEvent"/> class.
	/// </summary>
	/// <param name="interactions">The interactions.</param>
	public EnterInteractionRegionEvent(params Interaction[] interactions)
	{
		Interactions = interactions;
	}
}
