using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Sleep")]
public class SleepReaction : Reaction
{
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
		Player.Sleep();
	}
}
