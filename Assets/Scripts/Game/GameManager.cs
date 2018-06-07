using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private bool gameOver;

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
		EventManager.StopListening<GameOverEvent>(SetGameOver);
	}

	// Use this for initialization
	void Start()
	{
		// Start Event Listener
		EventManager.StartListening<GameOverEvent>(SetGameOver);
	}

	// Update is called once per frame
	void Update()
	{
		if (gameOver == true)
		{
			// Show GameOver Scene
		}
	}

	private void SetGameOver(GameOverEvent @event)
	{
		gameOver = true;
	}
}
