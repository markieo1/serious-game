using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Eat")]
public class EatReaction : Reaction
{
	/// <summary>
	/// The sugar
	/// </summary>
	public float Sugar;

	/// <summary>
	/// The player
	/// </summary>
	private PlayerController Player;

	/// <summary>
	/// The interactable object
	/// </summary>
	private GameObject InteractableObject;

	protected override void SpecificInit(MonoBehaviour monoBehaviour)
	{
		base.SpecificInit(monoBehaviour);

		// Find the player in the scene
		Player = PlayerController.GetPlayer();

		Component interactableComponent = monoBehaviour.GetComponentInParent(typeof(Interactable));
		if (!interactableComponent)
		{
			throw new NotSupportedException("Interactable component not found in parent");
		}

		InteractableObject = interactableComponent.gameObject;
	}

	public override void React()
	{
		Player.Eat(Sugar);

		// Finally destroy the element
		Destroy(InteractableObject);
		EventManager.TriggerEvent(new ExitInteractionRegionEvent());
	}
}
