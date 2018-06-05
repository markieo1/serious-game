using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Reactions/SwitchScene")]
public class SwitchSceneReaction : Reaction
{
	[Scene]
	public string LoadingScene;

	[Scene]
	public string NewScene;

	[Tag]
	public string SpawnPoint;

	public override void React(MonoBehaviour monoBehaviour)
	{
		SceneManager.LoadScene(LoadingScene, LoadSceneMode.Single);
		LoadingManager.Instance.SetSceneToLoad(NewScene);
		LoadingManager.Instance.SetSpawnPoint(SpawnPoint);
	}
}
