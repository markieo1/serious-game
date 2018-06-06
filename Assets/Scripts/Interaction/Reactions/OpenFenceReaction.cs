using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/OpenFence")]
public class OpenFenceReaction : Reaction
{

	public override void React(MonoBehaviour monoBehaviour)
	{
		Debug.Log("OpenFence reaction has been called");
	}
}
