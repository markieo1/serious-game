using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reaction : ScriptableObject
{
	public void Init()
	{
		SpecificInit();
	}

	/// <summary>
	/// Intializes for specific reactions
	/// </summary>
	protected virtual void SpecificInit()
	{ }

	/// <summary>
	/// Reacts and supplies the monobehaviour
	/// </summary>
	/// <param name="monoBehaviour">The mono behaviour.</param>
	public abstract void React(MonoBehaviour monoBehaviour);
}
