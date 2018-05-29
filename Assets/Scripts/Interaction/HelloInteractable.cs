using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloInteractable : InteractableBase
{
	public override void OnEnterInteractionRegion()
	{
		Debug.Log("Hello, interact!");
	}
}
