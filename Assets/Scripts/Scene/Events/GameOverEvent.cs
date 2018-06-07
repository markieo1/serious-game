using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEvent : EventBase
{
	public bool GameOver;

	public GameOverEvent(bool gameOver)
	{
		GameOver = gameOver;
	}
}
