using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour {

	public float DecayPerSecond;

	/// <summary>
	/// The player
	/// </summary>
	private PlayerController Player;

	// Use this for initialization
	void Start () {

		// Find the player in the scene
		Player = PlayerController.GetPlayer();

	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.collider.IsPlayer()) {

			float decay = DecayPerSecond * Time.deltaTime;
			
			Player.PlaySport(decay);
		}
	}
}
