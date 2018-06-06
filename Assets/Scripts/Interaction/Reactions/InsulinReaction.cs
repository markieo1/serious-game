using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Insulin")]
public class InsulinReaction : Reaction
{
	/// <summary>
	/// The sugar level to decrement by.
	/// </summary>
	public float Sugar;

	/// <summary>
	/// The warning message to display if the blood sugar would drop too low due to this action.
	/// </summary>
	public string WarningMessage;

	/// <summary>
	/// The player
	/// </summary>
	private PlayerController Player;

	protected override void SpecificInit(MonoBehaviour monoBehaviour)
	{
		base.SpecificInit(monoBehaviour);

		// Find the player in the scene
		Player = PlayerController.GetPlayer();
	}


	public override void React()
	{
		//Check if blood sugar level doesn't drop too low due to this action
		if (CharacterData.BloodSugarLevel - Sugar < 10)
		{
			EventManager.TriggerEvent(new ShowPopupEvent(PopupItem.Indefinitely(WarningMessage)));
		}
		else
		{
			Player.Insulin(Sugar);
		}
	}
}
