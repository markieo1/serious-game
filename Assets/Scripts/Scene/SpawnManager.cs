﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

	public GameObject Player;
	public List<GameObject> SpawnPoints;

	// Use this for initialization
	void Start()
	{
		//try
		//{
		string spawnTag = LoadingManager.Instance.GetSpawnPoint();

		GameObject spawnObject = SpawnPoints.FirstOrDefault(s => s.tag == spawnTag);

		if (spawnObject != null)
		{
			SetPlayerToSpawn(Player, spawnObject);
		}
		//}
		//catch
		//{
		//	Debug.LogError("Spawn point is not set, please correct this in the editor.");
		//}
	}

	public void SetPlayerToSpawn(GameObject player, GameObject spawn)
	{
		// Set the current player position and rotation
		player.transform.position = spawn.transform.position;
		player.transform.rotation = spawn.transform.rotation;
	}
}
