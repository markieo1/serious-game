using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private ITimeManager timeManager;

	public static GameManager Instance { get; protected set; }

	[Scene]
	public string GameOverScene;

	/// <summary>
	/// The minimum blood sugar level
	/// </summary>
	public float MinimumBloodSugarLevel;

	/// <summary>
	/// The maximum blood sugar level
	/// </summary>
	public float MaximumBloodSugarLevel;

	/// <summary>
	/// The blood sugar level sport limit
	/// </summary>
	public float BloodSugarLevelSportLimit;

	/// <summary>
	/// Gets or sets a value indicating whether the game is paused.
	/// </summary>
	public bool IsPaused { get; protected set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is game over.
	/// </summary>
	public bool IsGameOver { get; protected set; }

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

	/// <summary>
	/// Gets a value indicating whether this instance can play sport.
	/// </summary>
	public bool CanPlaySport
	{
		get
		{
			return timeManager.IsDay();
		}
	}
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
		timeManager = new TimeManager();

		ResetToInitial();
	}

	void OnDisable()
	{
		SceneManager.activeSceneChanged -= ChangedActiveScene;

		EventManager.StopListening<EnterInteractionRegionEvent>(OnEnterInteractionRegion);
		EventManager.StopListening<ExitInteractionRegionEvent>(OnExitInteractionRegion);
	}

	// Use this for initialization
	void Start()
	{
		// Listen for scene changes
		SceneManager.activeSceneChanged += ChangedActiveScene;

		// Start Event Listener
		EventManager.StartListening<EnterInteractionRegionEvent>(OnEnterInteractionRegion);
		EventManager.StartListening<ExitInteractionRegionEvent>(OnExitInteractionRegion);
	}

	// Update is called once per frame
	void Update()
	{
		timeManager.Tick();
		CheckPausing();
		CheckInteraction();
		CheckBloodSugar();
	}

	/// <summary>
	/// Resets the variables to initial.
	/// </summary>
	private void ResetToInitial()
	{
		IsPaused = false;
		IsGameOver = false;
		OpenedMenu = MenuType.None;
		interactionPossiblities = new List<Interaction>();
		CharacterData.ResetBloodSugar();
	}

	#region "Scene Switching"
	private void ChangedActiveScene(Scene current, Scene next)
	{
		ResetToInitial();
	}
	#endregion

	public TimeSpan GetTime()
	{
		return timeManager.GetTime();
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
		if (IsGameOver) return;

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

	#region "GameOver"
	private void OnGameOver()
	{
		// Play Gameover scene
		SceneManager.LoadScene(GameOverScene);
		Destroy(this);
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
		if (IsGameOver) return;

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
		Time.timeScale = 1;
		EventManager.TriggerEvent(new InteractionSelectorChangeEvent(false));
	}

	/// <summary>
	/// Opens the interaction.
	/// </summary>
	public void OpenInteraction()
	{
		OpenedMenu = MenuType.Interaction;
		Time.timeScale = 0;
		EventManager.TriggerEvent(new InteractionSelectorChangeEvent(true));
	}

	/// <summary>
	/// Called when [player interacted].
	/// </summary>
	public void OnPlayerInteracted()
	{
		Time.timeScale = 1;
		OpenedMenu = MenuType.None;
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

	#region "Blood Sugar"	
	/// <summary>
	/// Checks the blood sugar.
	/// </summary>
	private void CheckBloodSugar()
	{
		if (IsGameOver) return;

		if (IsPaused) return;

		if (CharacterData.BloodSugarLevel <= MinimumBloodSugarLevel || CharacterData.BloodSugarLevel >= MaximumBloodSugarLevel)
		{
			IsGameOver = true;
			OnGameOver();

			EventManager.TriggerEvent(new GameOverEvent());
		}
	}
	#endregion
}
