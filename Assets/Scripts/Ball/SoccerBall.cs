using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour {

	public float DecayPerSecond;
	public float SportLimit;

	/// <summary>
	/// The player
	/// </summary>
	private PlayerController Player;

	// Use this for initialization
	void Start () {

		// Find the player in the scene
		Player = PlayerController.GetPlayer();

	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnCollisionStay(Collision collision)
	{
		if (Player.bloosSugarlevel <= SportLimit)
		{
			EventManager.TriggerEvent(new ShowPopupEvent(PopupItem.Indefinitely("Jouw bloed suiker spiegel is te laag om te sporten.")));
		}

		if (collision.gameObject.tag == "Player") {
			float decay = DecayPerSecond * Time.deltaTime;
			
			Player.Insulin(DecayPerSecond);
		}
	}
}
