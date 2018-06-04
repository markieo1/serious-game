using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	/// <summary>
	/// The interactions
	/// </summary>
	public Interaction[] Interactions = new Interaction[0];

	/// <summary> 
	/// Gets or sets the collider. 
	/// </summary> 
	private Collider Collider { get; set; }

	private void Start()
	{
		// This way we ensure there is an collider 
		Collider[] colliders = GetComponents<Collider>();

		if (colliders.Length <= 0)
		{
			throw new NotSupportedException(string.Format("An collider is required to support interactable, object: {0}", name));
		}

		if (!colliders.Any(x => x.isTrigger))
		{
			throw new NotSupportedException(string.Format("An collider as trigger is required to support interactable, object: {0}", name));
		}

		Collider = colliders.FirstOrDefault(x => x.isTrigger);

		if (!Collider)
		{
			throw new NotSupportedException(string.Format("Collider configured as trigger was not found, object: {0}", name));
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.IsPlayer())
		{
			// Notify systems from interaction possiblities.
			EventManager.TriggerEvent(new EnterInteractionRegionEvent()
			{
				Interactions = Interactions
			});
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.IsPlayer())
		{
			// Notify systems there are no interaction possiblities.
			EventManager.TriggerEvent(new ExitInteractionRegionEvent());
		}
	}
}
