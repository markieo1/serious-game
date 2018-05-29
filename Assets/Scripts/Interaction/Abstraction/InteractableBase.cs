using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
	/// <summary>
	/// Called when an collider enters the interaction region.
	/// </summary>
	public virtual void OnEnterInteractionRegion() { }

	/// <summary>
	/// Called when an collider exists the interaction region.
	/// </summary>
	public virtual void OnExitInteractionRegion() { }

	protected bool IsInInteractionRegion { get; set; }

	/// <summary>
	/// Gets or sets the collider.
	/// </summary>
	private Collider Collider { get; set; }

	private void Start()
	{
		// This way we ensure there is an collider
		Collider = GetComponent<Collider>();

		if (!Collider)
		{
			throw new NotSupportedException(string.Format("An collider is required to support interactable, object: {0}", name));
		}

		if (!Collider.isTrigger)
		{
			throw new NotSupportedException(string.Format("An collider as trigger is required to support interactable, object: {0}", name));
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		IsInInteractionRegion = true;
		OnEnterInteractionRegion();
	}

	private void OnTriggerExit(Collider other)
	{
		IsInInteractionRegion = false;
		OnExitInteractionRegion();
	}
}
