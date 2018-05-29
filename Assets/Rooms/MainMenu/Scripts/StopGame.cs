using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StopGame : MonoBehaviour
{
	public void Stop_Click()
	{
#if UNITY_EDITOR
		if (EditorApplication.isPlaying)
		{
			EditorApplication.isPlaying = false;
		}
#endif
		Application.Quit();
	}
}
