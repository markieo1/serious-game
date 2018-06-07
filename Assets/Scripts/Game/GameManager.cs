using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; protected set; }

	public bool IsPaused { get; protected set; }
	public MenuType OpenedMenu { get; protected set; }
	public bool CanInteract
	{
		get
		{
			return interactionPossiblities.Any();
		}
	}

	private bool gameOver;
	private List<Interaction> interactionPossiblities;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void OnDisable()
	{
		EventManager.StopListening<GameOverEvent>(OnGameOver);
		EventManager.StopListening<EnterInteractionRegionEvent>(OnEnterInteractionRegion);
		EventManager.StopListening<ExitInteractionRegionEvent>(OnExitInteractionRegion);
	}

	// Use this for initialization
	void Start()
	{
		OpenedMenu = MenuType.None;
		interactionPossiblities = new List<Interaction>();

		// Start Event Listener
		EventManager.StartListening<GameOverEvent>(OnGameOver);
		EventManager.StartListening<EnterInteractionRegionEvent>(OnEnterInteractionRegion);
		EventManager.StartListening<ExitInteractionRegionEvent>(OnExitInteractionRegion);
	}

	// Update is called once per frame
	void Update()
	{
		CheckPausing();
		if (gameOver == true)
		{
			// Show GameOver Scene
		}
	}

	private void OnGameOver(GameOverEvent @event)
	{
		gameOver = true;
	}

	#region "Pausing"
	/// <summary>
	/// Pauses the game.
	/// </summary>
	public void Pause()
	{
		Time.timeScale = 0;
		EventManager.TriggerEvent(new GamePauseChangeEvent(true));
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	public void Unpause()
	{
		Time.timeScale = 1;
		EventManager.TriggerEvent(new GamePauseChangeEvent(false));
	}

	/// <summary>
	/// Checks the pausing.
	/// </summary>
	private void CheckPausing()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			if (IsPaused)
			{
				Unpause();
			}
			else
			{
				Pause();
			}
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			Pause();
		}
	}
	#endregion

	#region "Interaction"	
	/// <summary>
	/// Checks the interaction.
	/// </summary>
	private void CheckInteraction()
	{
		// If we are paused we cannot do any interactions
		if (IsPaused) return;

		// If we do not have any interactions we cannot interact anyway
		if (!interactionPossiblities.Any()) return;

		// If the interaction menu is open we can close, else we cant do anything
		if (OpenedMenu != MenuType.None && OpenedMenu != MenuType.Interaction) return;

		if (Input.GetButtonDown("Interact"))
		{
			EventManager.TriggerEvent(new OpenInteractionSelectorEvent());

		}
	}

	#region "Events"
	private void OnExitInteractionRegion(ExitInteractionRegionEvent e)
	{
		interactionPossiblities.Clear();
	}

	private void OnEnterInteractionRegion(EnterInteractionRegionEvent e)
	{
		interactionPossiblities.Clear();
		interactionPossiblities.AddRange(e.Interactions);
	}
	#endregion
	#endregion
}
