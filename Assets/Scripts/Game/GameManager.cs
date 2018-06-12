using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private ITimeManager timeManager;

	public static GameManager Instance { get; protected set; }

	/// <summary>
	/// Gets or sets a value indicating whether the game is paused.
	/// </summary>
	public bool IsPaused { get; protected set; }

	/// <summary>
	/// Gets a value indicating whether any menu is open.
	/// </summary>
	public bool AnyMenuOpen { get { return OpenedMenu != MenuType.None; } }

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
		timeManager = new TimeManager();

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
		timeManager.Tick();
		CheckPausing();
		CheckInteraction();
		if (gameOver == true)
		{
			// Show GameOver Scene
		}
	}

	private void OnGameOver(GameOverEvent @event)
	{
		gameOver = true;
	}

	public TimeSpan GetTime()
	{
		return timeManager.GetTime();
	}

	public void SetTimeSpeed(float timeSpeed)
	{
		timeManager.SetTimeSpeed(timeSpeed);
	}

	#region "Pausing"
	/// <summary>
	/// Pauses the game.
	/// </summary>
	public void Pause()
	{
		timeManager.Pause();
		EventManager.TriggerEvent(new GamePauseChangeEvent(true));
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	public void Unpause()
	{
		timeManager.Unpause();
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
				// Not paused, check if we have any menu open
				if (AnyMenuOpen)
				{
					// We should close the menu
					CloseMenu(OpenedMenu);
				}

				// We have nothing open so we can pause
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

	#region "Menu"
	private void CloseMenu(MenuType menuType)
	{
		// No menu is open
		if (menuType == MenuType.None) return;

		switch (menuType)
		{
			case MenuType.Interaction:
				{
					CloseInteraction();
					break;
				}
			default:
				{
					throw new NotImplementedException("Closing menu type: " + menuType + " is not supported!");
				}
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
			bool isInteractionMenuOpen = OpenedMenu == MenuType.Interaction;
			if (isInteractionMenuOpen)
			{
				// We are closing
				CloseInteraction();
			}
			else
			{
				// We are opening
				OpenInteraction();
			}
		}
	}

	/// <summary>
	/// Closes the interaction.
	/// </summary>
	public void CloseInteraction()
	{
		OpenedMenu = MenuType.None;
		EventManager.TriggerEvent(new InteractionSelectorChangeEvent(false));
	}

	/// <summary>
	/// Opens the interaction.
	/// </summary>
	public void OpenInteraction()
	{
		OpenedMenu = MenuType.Interaction;
		EventManager.TriggerEvent(new InteractionSelectorChangeEvent(true));
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
