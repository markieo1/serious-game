﻿using System.Collections;
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
	private void Awake()
	{
		CanvasGroup = GetComponent<CanvasGroup>();
		HideCanvasGroup();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			if (isShowing)
			{
				Unpause();
			}
			else
			{
				Pause();
			}
		}
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	public void Pause()
	{
		isShowing = true;
		// TODO: Move to GameManager
		Time.timeScale = 0;
		ShowCanvasGroup();
		EventManager.TriggerEvent(new GamePauseChangeEvent(true));
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	public void Unpause()
	{
		isShowing = false;
		// TODO: Move to GameManager
		Time.timeScale = 1;
		HideCanvasGroup();
		EventManager.TriggerEvent(new GamePauseChangeEvent(false));
	}

	/// <summary>
	/// Navigates to main menu.
	/// </summary>
	public void NavigateToMainMenu()
	{
		SceneManager.LoadScene(LoadingScene, LoadSceneMode.Single);
		LoadingManager.Instance.SetSceneToLoad(MainMenuScene);
		Unpause();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Pause();
		}
	}

	/// <summary>
	/// Hides the canvas group.
	/// </summary>
	private void HideCanvasGroup()
	{
		CanvasGroup.alpha = 0;
		CanvasGroup.interactable = false;
	}

	/// <summary>
	/// Shows the canvas group.
	/// </summary>
	private void ShowCanvasGroup()
	{
		CanvasGroup.alpha = 1;
		CanvasGroup.interactable = true;
	}
}
