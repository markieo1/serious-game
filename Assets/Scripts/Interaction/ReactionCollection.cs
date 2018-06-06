using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCollection : MonoBehaviour
{
	/// <summary>
	/// The reactions
	/// </summary>
	public Reaction[] reactions = new Reaction[0];

	/// <summary>
	/// The audio source for reactions that play sounds
	/// </summary>
	public AudioSource AudioSource;

	/// <summary>
	/// The animator source for reactions that play animation
	/// </summary>
	public Animator AnimationSource;

	private void Start()
	{
		for (int i = 0; i < reactions.Length; i++)
		{
			reactions[i].Init(this);
		}
	}

	/// <summary>
	/// Reacts.
	/// </summary>
	public void React()
	{
		for (int i = 0; i < reactions.Length; i++)
		{
			reactions[i].React();
		}
	}
}
