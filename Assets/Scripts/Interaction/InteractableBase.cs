using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
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
		OnInteract();
	}

	public abstract void OnInteract();
}
