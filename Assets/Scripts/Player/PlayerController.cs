using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
	private bool hasInteractions;
	private bool isPaused = false;
	public float BloodSugarlevel { get { return CharacterData.BloodSugarLevel; } }

	private void Start()
	{
		EventManager.StartListening<EnterInteractionRegionEvent>(EnteringInteractionRegionEvent);
		EventManager.StartListening<ExitInteractionRegionEvent>(ExitInteractionRegionEvent);
		EventManager.StartListening<GamePauseChangeEvent>(OnGamePauseChangeEvent);
	}

	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
	{
		if (hasInteractions && !isPaused && Input.GetButtonDown("Interact"))
		{
			EventManager.TriggerEvent(new OpenInteractionSelectorEvent());
		}
	}

	private void OnGamePauseChangeEvent(GamePauseChangeEvent e)
	{
		isPaused = e.IsPaused;
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
		CharacterData.IncrementBloodSugar(sugar);
	}

	/// <summary>
	/// Inject insulin, which adjusts the sugar level.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public void Insulin(float sugar)
	{
		CharacterData.DecrementBloodSugar(sugar);
	}

	/// <summary>
	/// Lower sugar level when playing sport
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public void PlaySport(float sugar, float sportLimit )
	{
		// To Do: Check for day and night
		// Move gameover to GameManager
		// Warning should be configurable

		if (BloodSugarlevel <= 20)
		{
			EventManager.TriggerEvent(new GameOverEvent());
		}

		if (BloodSugarlevel <= sportLimit)
		{
			EventManager.TriggerEvent(new ShowPopupEvent(PopupItem.Indefinitely("Jouw bloed suiker spiegel is te laag om te sporten.")));
		}

		CharacterData.DecrementBloodSugar(sugar);
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
