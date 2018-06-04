using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interaction
{
	/// <summary>
	/// The action
	/// </summary>
	public string Action;

	/// <summary>
	/// The reactions
	/// </summary>
	public ReactionCollection Reactions;

	/// <summary>
	/// Interacts.
	/// </summary>
	public void Interact()
	{
		if (Reactions)
		{
			Reactions.React();
		}
	}
}
