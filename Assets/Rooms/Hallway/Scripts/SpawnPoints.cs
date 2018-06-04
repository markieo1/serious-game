using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
	public GameObject player;
	public GameObject spawnA;

	// Use this for initialization
	void Start()
	{
		string spawn = LoadingManager.Instance.GetSpawnPoint();

		if (spawn == spawnA.tag)
		{
			SetPlayerToSpawn(player, spawnA);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetPlayerToSpawn(GameObject player, GameObject spawn)
	{
		// Get the position of the spawn location
		var xPos = spawn.transform.position.x;
		var yPos = spawn.transform.position.y;
		var zPos = spawn.transform.position.z;

		// Get the rotation of the spawn location
		var xRot = spawn.transform.rotation.x;
		var yRot = spawn.transform.rotation.y;
		var zRot = spawn.transform.rotation.z;
		var wRot = spawn.transform.rotation.w;

		// Set the current player position and rotation
		player.transform.position = new Vector3(xPos, yPos, zPos);
		player.transform.rotation = new Quaternion(xRot, yRot, zRot, wRot);
	}
}
