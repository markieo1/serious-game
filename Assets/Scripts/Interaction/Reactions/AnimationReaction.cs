using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Animation")]
public class AnimationReaction : Reaction
{
	private Animator anim;

	public string trigger;

	protected override void SpecificInit(MonoBehaviour monoBehaviour)
	{
		base.SpecificInit(monoBehaviour);

		ReactionCollection collection = monoBehaviour.GetComponent<ReactionCollection>();

		anim = collection.AnimationSource;

		if (!anim)
		{
			throw new NotSupportedException("An animator on : \"" + monoBehaviour.name + "\" is required for the animation to work.");
		}

	}

	public override void React()
	{
		if (string.IsNullOrEmpty(trigger))
		{
			throw new ArgumentNullException("Please specify a trigger");
		}

		anim.SetTrigger(trigger);
	}
}
