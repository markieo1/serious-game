using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
	private void Update()
	{
		bool value = Input.GetButton("Interact");

		if (value)
		{
			EventManager.TriggerEvent(new OpenInteractionSelectorEvent());
		}
	}
}
