using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Example")]
public class ExampleReaction : Reaction
{
	public override void React()
	{
		Debug.Log("Example reaction has been called");
	}
}
