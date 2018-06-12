using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.UIElements;

public class PlayerController : MonoBehaviour
{
	public float BloodSugarlevel { get { return CharacterData.BloodSugarLevel; } }
	public string WarningBloodSugarLevelBelowSportLimit;

	/// <summary>
	/// Eats, which adjusts the sugar level.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public void Eat(float sugar)
	{
		CharacterData.IncrementBloodSugar(sugar, SugarLevelInstigator.FOOD);
	}

	/// <summary>
	/// Inject insulin, which adjusts the sugar level.
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public void Insulin(float sugar)
	{
		CharacterData.DecrementBloodSugar(sugar, SugarLevelInstigator.INSULIN);
	}

	/// <summary>
	/// Lower sugar level when playing sport
	/// </summary>
	/// <param name="sugar">The sugar.</param>
	public void PlaySport(float sugar)
	{
		if (!GameManager.Instance.CanPlaySport) return;

		if (BloodSugarlevel <= GameManager.Instance.BloodSugarLevelSportLimit)
		{
			EventManager.TriggerEvent(new ShowPopupEvent(PopupItem.Indefinitely(WarningBloodSugarLevelBelowSportLimit)));
			return;
		}

		CharacterData.DecrementBloodSugar(sugar, SugarLevelInstigator.EXERCISE);
	}

	/// <summary>
	/// Lets the player sleep
	/// </summary>
	public void Sleep()
	{
		CharacterData.ResetBloodSugar();

		// TODO: Implement the time changes
	}

	/// <summary>
	/// Gets the player.
	/// </summary>
	/// <returns></returns>
	public static PlayerController GetPlayer()
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

		// Ensure the player obj has an player controller. 
		PlayerController controller = playerObj.GetComponent<PlayerController>();
		if (!controller)
		{
			throw new MissingComponentException("Player controller not found, on gameobject with tag \"Player\"");
		}

		return controller;
	}
}
