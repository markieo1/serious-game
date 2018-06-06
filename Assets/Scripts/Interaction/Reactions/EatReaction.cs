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

	protected override void SpecificInit(MonoBehaviour monoBehaviour)
	{
		base.SpecificInit(monoBehaviour);

		// Find the player in the scene
		Player = PlayerController.GetPlayer();
	}


	public override void React()
	{
		Player.Eat(Sugar);
	}
}
