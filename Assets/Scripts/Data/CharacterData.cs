﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to hold various character-related data between scenes.
/// </summary>
public static class CharacterData
{
	/// <summary>
	/// The initial blood sugar level
	/// </summary>
	public const float InitialBloodSugarLevel = 50;

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
		ResetBloodSugar();
	}

	/// <summary>
	/// Increments the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public static void IncrementBloodSugar(float sugar)
	{
		float oldSugar = CharacterData.BloodSugarLevel;
		CharacterData.BloodSugarLevel += sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel, oldSugar));
	}

	/// <summary>
	/// Increments the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	/// <param name="instigator">The type of instigator that caused the change.</param>
	public static void IncrementBloodSugar(float sugar, SugarLevelInstigator instigator)
	{
		float oldSugar = CharacterData.BloodSugarLevel;
		CharacterData.BloodSugarLevel += sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel, oldSugar, instigator));
	}

	/// <summary>
	/// Decrements the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public static void DecrementBloodSugar(float sugar)
	{
		float oldSugar = CharacterData.BloodSugarLevel;
		CharacterData.BloodSugarLevel -= sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel, oldSugar));
	}

	/// <summary>
	/// Decrements the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	/// <param name="instigator">The type of instigator that caused the change.</param>
	public static void DecrementBloodSugar(float sugar, SugarLevelInstigator instigator)
	{
		float oldSugar = CharacterData.BloodSugarLevel;
		CharacterData.BloodSugarLevel -= sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel, oldSugar, instigator));
	}

	/// <summary>
	/// Resets the blood sugar.
	/// </summary>
	public static void ResetBloodSugar()
	{
		BloodSugarLevel = InitialBloodSugarLevel;
		EventManager.TriggerEvent(new SugarChangedEvent(BloodSugarLevel, BloodSugarLevel, SugarLevelInstigator.INITIAL));
	}
}

public enum CharacterSelection
{
	MALE,
	FEMALE
}