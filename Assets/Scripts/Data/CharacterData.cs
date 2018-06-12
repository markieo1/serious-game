using System.Collections;
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

	/// <summary>
	/// The percentage below which the blood sugar level is considered "low."
	/// </summary>
	public const float BloodSugarLowPercentage = 20;

	/// <summary>
	/// The percentage above which the blood sugar level is considered "high".
	/// </summary>
	public const float BloodSugarHighPercentage = 80;

	/// <summary>
	/// Gets the blood sugar percentage, where 0% is the absolute minimum and 100% is the absolute maximum.
	/// </summary>
	public static float BloodSugarPercentage
	{
		get
		{
			return (BloodSugarLevel - GameManager.Instance.MinimumBloodSugarLevel) / (GameManager.Instance.MaximumBloodSugarLevel - GameManager.Instance.MinimumBloodSugarLevel);
		}
	}

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
		bool isAlreadyHigh = false;
		if (BloodSugarPercentage > BloodSugarHighPercentage)
		{
			isAlreadyHigh = true;
		}

		CharacterData.BloodSugarLevel += sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel, oldSugar));

		// If blood sugar is already high, don't send out another event.
		if (BloodSugarPercentage > BloodSugarHighPercentage && !isAlreadyHigh)
		{
			EventManager.TriggerEvent(new SugarHighEvent(CharacterData.BloodSugarLevel, oldSugar));
		}
	}

	/// <summary>
	/// Increments the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	/// <param name="instigator">The type of instigator that caused the change.</param>
	public static void IncrementBloodSugar(float sugar, SugarLevelInstigator instigator)
	{
		float oldSugar = CharacterData.BloodSugarLevel;
		bool isAlreadyHigh = false;
		if (BloodSugarPercentage > BloodSugarHighPercentage)
		{
			isAlreadyHigh = true;
		}

		CharacterData.BloodSugarLevel += sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel, oldSugar, instigator));

		// If blood sugar is already high, don't send out another event.
		if (BloodSugarPercentage > BloodSugarHighPercentage && !isAlreadyHigh)
		{
			EventManager.TriggerEvent(new SugarHighEvent(CharacterData.BloodSugarLevel, oldSugar, instigator));
		}
	}

	/// <summary>
	/// Decrements the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public static void DecrementBloodSugar(float sugar)
	{
		float oldSugar = CharacterData.BloodSugarLevel;
		bool isAlreadyLow = false;
		if(BloodSugarPercentage < BloodSugarLowPercentage)
		{
			isAlreadyLow = true;
		}

		CharacterData.BloodSugarLevel -= sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel, oldSugar));

		// If blood sugar is already low, don't send out another event.
		if(BloodSugarPercentage < BloodSugarLowPercentage && !isAlreadyLow)
		{
			EventManager.TriggerEvent(new SugarLowEvent(CharacterData.BloodSugarLevel, oldSugar));
		}
	}

	/// <summary>
	/// Decrements the blood sugar.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	/// <param name="instigator">The type of instigator that caused the change.</param>
	public static void DecrementBloodSugar(float sugar, SugarLevelInstigator instigator)
	{
		float oldSugar = CharacterData.BloodSugarLevel;
		bool isAlreadyLow = false;
		if (BloodSugarPercentage < BloodSugarLowPercentage)
		{
			isAlreadyLow = true;
		}

		CharacterData.BloodSugarLevel -= sugar;
		EventManager.TriggerEvent(new SugarChangedEvent(CharacterData.BloodSugarLevel, oldSugar, instigator));

		// If blood sugar is already low, don't send out another event.
		if (BloodSugarPercentage < BloodSugarLowPercentage && !isAlreadyLow)
		{
			EventManager.TriggerEvent(new SugarLowEvent(CharacterData.BloodSugarLevel, oldSugar, instigator));
		}
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