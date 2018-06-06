using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to hold various character-related data between scenes.
/// </summary>
public static class CharacterData
{
	/// <summary>
	/// Gets or sets the character selection.
	/// </summary>
	public static CharacterSelection CharacterSelection { get; set; }

	/// <summary>
	/// Gets or sets the blood sugar level.
	/// </summary>
	public static float BloodSugarLevel { get; private set; }

	static CharacterData()
	{
		BloodSugarLevel = 50;
		EventManager.TriggerEvent(new SugarChangedEvent(BloodSugarLevel));
	}

	/// <summary>
	/// Increments the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public static void IncrementBloodSugar(float sugar)
	{
		CharacterData.BloodSugarLevel += sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel));
	}

	/// <summary>
	/// Decrements the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public static void DecrementBloodSugar(float sugar)
	{
		CharacterData.BloodSugarLevel -= sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel));
	}
}

public enum CharacterSelection
{
	MALE,
	FEMALE
}