using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleReaction : Reaction
{
	public override void React(MonoBehaviour monoBehaviour)
	{
		Debug.Log("Example reaction has been called");
	}
}
