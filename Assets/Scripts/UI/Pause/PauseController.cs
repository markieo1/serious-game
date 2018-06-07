using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class PauseController : MonoBehaviour
{
	[Scene]
	public string LoadingScene;

	[Scene]
	public string MainMenuScene;

	private CanvasGroup CanvasGroup;
	private bool isShowing = false;

	private void Start()
	{
		CanvasGroup = GetComponent<CanvasGroup>();
		CanvasGroup.Hide();

		// Register the listener for GamePauseChange
		EventManager.StartListening<GamePauseChangeEvent>(OnPauseChanged);
	}

	private void OnDestroy()
	{
		EventManager.StopListening<GamePauseChangeEvent>(OnPauseChanged);
	}

	/// <summary>
	/// Occures when the unpause button is clicked.
	/// </summary>
	public void UnpauseClick()
	{
		GameManager.Instance.Unpause();
	}

	/// <summary>
	/// Called when pausing is changed.
	/// </summary>
	/// <param name="event">The event.</param>
	private void OnPauseChanged(GamePauseChangeEvent @event)
	{
		if (@event.IsPaused)
		{
			Pause();
		}
		else
		{
			Unpause();
		}
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	private void Pause()
	{
		// Check if we are not already showing
		if (isShowing) return;

		isShowing = true;
		CanvasGroup.Show();
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	private void Unpause()
	{
		// Check if we are not already invisible
		if (!isShowing) return;

		isShowing = false;
		CanvasGroup.Hide();
	}

	/// <summary>
	/// Navigates to main menu.
	/// </summary>
	private void NavigateToMainMenu()
	{
		GameManager.Instance.Unpause();

		SceneManager.LoadScene(LoadingScene, LoadSceneMode.Single);
		LoadingManager.Instance.SetSceneToLoad(MainMenuScene);
	}
}
