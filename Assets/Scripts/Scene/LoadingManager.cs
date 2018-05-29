using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>
	/// The instance.
	/// </value>
	public static LoadingManager Instance { get; private set; }

	private string nextScene;

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

	/// <summary>
	/// Sets the scene to load.
	/// </summary>
	/// <param name="scene">The scene.</param>
	public void SetSceneToLoad(string scene)
	{
		nextScene = scene;
	}

	/// <summary>
	/// Gets the next scene.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="System.NotSupportedException">Next scene is not set, please correct this in the editor.</exception>
	public string GetNextScene()
	{
		if (string.IsNullOrEmpty(nextScene))
		{
			throw new NotSupportedException("Next scene is not set, please correct this in the editor.");
		}

		return nextScene;
	}
}
