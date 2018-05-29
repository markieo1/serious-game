using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	private LoadingManager _loadingManager;

	[Scene]
	public string LoadingScene;

	[Scene]
	public string NewScene;

	public void Start_Click()
	{
		_loadingManager.SetSceneToLoad(NewScene);
	}

	private void Start()
	{
		_loadingManager = LoadingManager.Instance;
	}
}
