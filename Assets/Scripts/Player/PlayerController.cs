using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
	{
		bool value = Input.GetButtonDown("Interact");

		if (value)
		{
			EventManager.TriggerEvent(new OpenInteractionSelectorEvent());
		}
	}
}
