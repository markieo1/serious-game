using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
	void OnMouseDown()
	{
		this.OnInteract();
	}

	public abstract void OnInteract();
}
