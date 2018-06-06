using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Audio")]
public class AudioReaction : Reaction
{
	/// <summary>
	/// The audio clip
	/// </summary>
	public AudioClip AudioClip;

	/// <summary>
	/// The delay
	/// </summary>
	public float delay;

	/// <summary>
	/// The audio source
	/// </summary>
	private AudioSource audioSource;

	protected override void SpecificInit(MonoBehaviour monoBehaviour)
	{
		base.SpecificInit(monoBehaviour);

		ReactionCollection collection = monoBehaviour.GetComponent<ReactionCollection>();
		audioSource = collection.AudioSource;

		if (!audioSource)
		{
			throw new NotSupportedException("An audio source on the reaction collection of: \"" + monoBehaviour.name + "\" is required for the audio reaction to work.");
		}
	}

	public override void React()
	{
		audioSource.clip = AudioClip;
		audioSource.PlayDelayed(delay);
	}
}
