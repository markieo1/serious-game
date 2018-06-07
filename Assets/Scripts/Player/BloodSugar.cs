﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSugar : MonoBehaviour
{

	public float MinimumBelowLevel;
	public float MaximumAboveLevel;

	/// <summary>
	/// How much the blood sugar level should decay per second.
	/// </summary>
	public float DecayPerSecond;

	/// <summary>
	/// How much to multiply the blood sugar decay by when walking.
	/// </summary>
	public float WalkingDecayMultiplier;

	/// <summary>
	/// How much to multiply the blood sugar decay by when running.
	/// </summary>
	public float RunningDecayMultiplier;

	private Animator animator;

	// Use this for initialization
	void Start()
	{
		this.animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		float decay = DecayPerSecond * Time.deltaTime;

		CharacterData.DecrementBloodSugar(decay);

		if (CharacterData.BloodSugarLevel <= MinimumBelowLevel || CharacterData.BloodSugarLevel >= MaximumAboveLevel)
		{
			EventManager.TriggerEvent(new GameOverEvent(true));
		}

		animator.SetFloat("BloodSugar", CharacterData.BloodSugarLevel);
	}
}
