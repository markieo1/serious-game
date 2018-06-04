﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
	private bool hasInteractions;

	private void Start()
	{
		EventManager.StartListening<EnterInteractionRegionEvent>(EnteringInteractionRegionEvent);
		EventManager.StartListening<ExitInteractionRegionEvent>(ExitInteractionRegionEvent);
	}

	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
	{
		if (hasInteractions && Input.GetButtonDown("Interact"))
		{
			EventManager.TriggerEvent(new OpenInteractionSelectorEvent());
		}
	}

	private void EnteringInteractionRegionEvent(EventBase @event)
	{
		var t = (EnterInteractionRegionEvent)@event;
		hasInteractions = t.Interactions.Any();
	}

	private void ExitInteractionRegionEvent(EventBase @event)
	{
		hasInteractions = false;
	}

	private void OnDisable()
	{
		EventManager.StopListening<EnterInteractionRegionEvent>(EnteringInteractionRegionEvent);
		EventManager.StopListening<ExitInteractionRegionEvent>(ExitInteractionRegionEvent);
	}

	/// <summary>
	/// Eats, which adjusts the sugar level.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public void Eat(float sugar)
	{
		CharacterData.BloodSugarLevel += sugar;
		EventManager.TriggerEvent(new SugarChangedEvent()
		{
			Value = CharacterData.BloodSugarLevel
		});
	}

	/// <summary>
	/// Gets the player.
	/// </summary>
	/// <returns></returns>
	public static PlayerController GetPlayer()
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

		// Ensure the player obj has an player controller. 
		PlayerController controller = playerObj.GetComponent<PlayerController>();
		if (!controller)
		{
			throw new MissingComponentException("Player controller not found, on gameobject with tag \"Player\"");
		}

		return controller;
	}
}
