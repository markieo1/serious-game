﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to hold various character-related data between scenes.
/// </summary>
public static class CharacterData
{
	public static CharacterSelection CharacterSelection { get; set; }

	/// <summary>
	/// Gets or sets the blood sugar level.
	/// </summary>
	public static float BloodSugarLevel { get; set; }
}

public enum CharacterSelection
{
	MALE,
	FEMALE
}