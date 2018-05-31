﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCollection : MonoBehaviour
{
	/// <summary>
	/// The reactions
	/// </summary>
	public Reaction[] reactions = new Reaction[0];

	private void Start()
	{
		for (int i = 0; i < reactions.Length; i++)
		{
			reactions[i].Init();
		}
	}

	/// <summary>
	/// Reacts.
	/// </summary>
	public void React()
	{
		for (int i = 0; i < reactions.Length; i++)
		{
			reactions[i].React(this);
		}
	}
}
