using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reaction : ScriptableObject
{
	protected MonoBehaviour MonoBehaviour { get; set; }
	public void Init(MonoBehaviour monoBehaviour)
	{
		MonoBehaviour = monoBehaviour;
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
