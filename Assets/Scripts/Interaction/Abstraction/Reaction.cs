using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reaction : ScriptableObject
{
	public void Init(MonoBehaviour monoBehaviour)
	{
		SpecificInit(monoBehaviour);
	}

	/// <summary>
	/// Intializes for specific reactions
	/// </summary>
	/// <param name="monoBehaviour">The mono behaviour.</param>
	protected virtual void SpecificInit(MonoBehaviour monoBehaviour)
	{ }

	/// <summary>
	/// Reacts and supplies the monobehaviour
	/// </summary>
	public abstract void React();
}
