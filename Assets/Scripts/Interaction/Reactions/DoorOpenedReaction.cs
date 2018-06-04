using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpenedReaction : Reaction
{
	[Scene]
	private string loadingScene = "Loading";

	[Scene]
	private string newScene = "Hallway";

	private string spawnPoint = "SpawnA";

	public override void React(MonoBehaviour monoBehaviour)
	{
		SceneManager.LoadScene(loadingScene, LoadSceneMode.Single);
		LoadingManager.Instance.SetSceneToLoad(newScene);
		LoadingManager.Instance.SetSpawnPoint(spawnPoint);
	}
}
