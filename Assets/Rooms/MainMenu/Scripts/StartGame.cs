using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{											 
	[Scene]
	public string LoadingScene;

	[Scene]
	public string NewScene;

	public void Start_Click()
	{												  
		SceneManager.LoadScene(LoadingScene, LoadSceneMode.Single);
		LoadingManager.Instance.SetSceneToLoad(NewScene);
	}
}
