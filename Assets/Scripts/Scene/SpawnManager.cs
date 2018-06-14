﻿using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public float TimeToSpawnInSeconds = 10;
    public GameObject BoyPlayer;
    public GameObject GirlPlayer;

    public List<GameObject> SpawnPoints;
	public List<Transform> TransformPoints;
	public List<GameObject> FoodObjects;

    private GameObject Player;

    Dictionary<Transform, Interactable> TransformPointsAndFoodObjects;

	// Use this for initialization
	void Start()
	{
        var PlayerCharacter = (int)CharacterData.CharacterSelection;
        var PlayerCamera = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
        var CineMachineCamera = PlayerCamera.GetComponent<CinemachineFreeLook>();

        if (PlayerCharacter == 0)
        {
            Player = BoyPlayer;
            Instantiate(Player);            
        }
        if(PlayerCharacter == 1)
        {
            Player = GirlPlayer;            
            Instantiate(Player);
        }

        //Get the cloned Player from the scene
        var ClonedPlayer = GameObject.FindGameObjectWithTag("Player");

        // get the armature children from the player component
        var Armature = ClonedPlayer.transform.GetChild(0);

        // set the Camera values follow and lookat
        CineMachineCamera.Follow = ClonedPlayer.transform;
        CineMachineCamera.LookAt = Armature;


        TransformPointsAndFoodObjects = new Dictionary<Transform, Interactable>();

        // loop through points insert in dictionary
        TransformPoints.ForEach(x =>
        {
            TransformPointsAndFoodObjects.Add(x, null);
        });


        if (LoadingManager.Instance.GetSpawnCheck())
        {
            string spawnTag = LoadingManager.Instance.GetSpawnPoint();

            GameObject spawnObject = SpawnPoints.FirstOrDefault(s => s.tag == spawnTag);

            if (spawnObject != null)
            {
                SetPlayerToSpawn(Player, spawnObject);
            }
        }

        if (TransformPoints != null && TransformPoints.Count > 0 && FoodObjects != null && FoodObjects.Count > 0)
        {
            InvokeRepeating("CheckAndSpawnFood", 0, TimeToSpawnInSeconds);
        }
    }

    public void CheckAndSpawnFood()
    {
        // Check in dictionary if value is null, if so spawn random

        for (int i = 0; i < TransformPointsAndFoodObjects.Count; i++)
        {
            var x = TransformPointsAndFoodObjects.ElementAtOrDefault(i);
            if (x.Value)
            {
                continue;
            }
            else
            {
                int randomNumber = Random.Range(0, FoodObjects.Count);
                var t = Instantiate(FoodObjects[randomNumber], x.Key.position, Quaternion.identity);
                Interactable comp = t.GetComponent<Interactable>();
                TransformPointsAndFoodObjects[x.Key] = comp;
            }
        }
    }

    public void SetPlayerToSpawn(GameObject player, GameObject spawn)
    {
        // Set the current player position and rotation
        player.transform.position = spawn.transform.position;
        player.transform.rotation = spawn.transform.rotation;
    }
}
