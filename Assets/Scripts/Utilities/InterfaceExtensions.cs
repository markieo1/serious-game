using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{	
	public static List<T> GetAbstractions<T>(this GameObject objectToSearch) where T : class
	{
		MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
		List<T> resultList = new List<T>();
		foreach (MonoBehaviour mb in list)
		{
			if (mb is T)
			{
				//found one
				resultList.Add((T)((System.Object)mb));
			}
		}

		return resultList;
	}
}
