using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; protected set; }

	private bool gameOver;
	public bool IsPaused { get; protected set; }

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
	}

	// Use this for initialization
	void Start()
	{
		// Start Event Listener
		EventManager.StartListening<GameOverEvent>(OnGameOver);
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
}
