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
	/// The player
	/// </summary>
	private PlayerController Player;

	protected override void SpecificInit()
	{
		base.SpecificInit();

		// Find the player in the scene
		Player = PlayerController.GetPlayer();
	}


	public override void React(MonoBehaviour monoBehaviour)
	{
		//Check if blood sugar level doesn't drop too low due to this action
		if (CharacterData.BloodSugarLevel - Sugar < 10)
		{
			EventManager.TriggerEvent(new ShowPopupEvent(PopupItem.Indefinitely("Je bloedsuikerspiegel is te laag om dit te kunnen doen.")));
		}
		else
		{
			Player.Insulin(Sugar);
		}
	}
}
