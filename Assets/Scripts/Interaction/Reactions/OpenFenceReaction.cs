using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/OpenFence")]
public class OpenFenceReaction : Reaction
{

	private Animator anim;

	public override void React(MonoBehaviour monoBehaviour)
	{
		anim = monoBehaviour.GetComponentInParent<Animator>();

		if (!anim.GetBool("IsOpen"))
		{
			anim.SetBool("IsOpen", true);
		}else
		{
			anim.SetBool("IsOpen", false);
		}
	}
}
