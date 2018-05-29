using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour
{
	public void Stop_Click()
	{
#if DEBUG
		Debug.Log("Application exited");
#else
		Application.Quit();
	}
#endif
	}
}
