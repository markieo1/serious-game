using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSugar : MonoBehaviour {

	/// <summary>
	/// How much the blood sugar level should decay per second.
	/// </summary>
	public double DecayPerSecond;

	/// <summary>
	/// How much to multiply the blood sugar decay by when walking.
	/// </summary>
	public double WalkingDecayMultiplier;

	/// <summary>
	/// How much to multiply the blood sugar decay by when running.
	/// </summary>
	public double RunningDecayMultiplier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		double decay = DecayPerSecond * Time.deltaTime;

		CharacterData.BloodSugarLevel -= decay;
	}

	public double GetBloodSugar()
	{
		return CharacterData.BloodSugarLevel;
	}
}
