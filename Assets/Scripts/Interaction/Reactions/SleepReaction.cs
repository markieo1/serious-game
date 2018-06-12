using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Sleep")]
public class SleepReaction : Reaction
{
	/// <summary>
	/// The start reaction
	/// </summary>
	public FadeReaction StartReaction;

	/// <summary>
	/// The end reaction
	/// </summary>
	public FadeReaction EndReaction;

	/// <summary>
	/// The sleep duration
	/// </summary>
	public float SleepDuration;

	/// <summary>
	/// The player
	/// </summary>
	private PlayerController Player;

	protected override void SpecificInit(MonoBehaviour monoBehaviour)
	{
		base.SpecificInit(monoBehaviour);

		// Find the player in the scene
		Player = PlayerController.GetPlayer();

		// Init the other reactions
		StartReaction.Init(monoBehaviour);
		EndReaction.Init(monoBehaviour);
	}

	public override void React()
	{
		// Since we now know how long the start reaction is we can correctly end it
		StartReaction.React();

		MonoBehaviour.StartCoroutine(EndSleepCoroutine(SleepDuration));

		Player.Sleep();
	}

	private IEnumerator EndSleepCoroutine(float sleepDuration)
	{
		yield return new WaitForSecondsRealtime(sleepDuration);

		EndReaction.React();
	}
}
