using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
	private Scene _gameScene;
	private Scene _loadScene;

	public void Start_Click()
	{
		// TODO: Modify Screennames (loading and entry screen)
		_gameScene = SceneManager.GetSceneByName("GameScene");
		_loadScene = SceneManager.GetSceneByName("LoadScene");
	}

	private void Update()
	{
		if (_gameScene.isLoaded)
		{
			SceneManager.UnloadSceneAsync(_loadScene);
		}
	}
}
