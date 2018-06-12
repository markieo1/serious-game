using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class AIAdvice : ScriptableObject
{
	public string AdviceText;
	
	public void Init(MonoBehaviour monoBehaviour)
	{
		SpecificInit(monoBehaviour);
	}

	/// <summary>
	/// Intializes for specific advice
	/// </summary>
	/// <param name="monoBehaviour">The mono behaviour.</param>
	protected virtual void SpecificInit(MonoBehaviour monoBehaviour)
	{ }

	/// <summary>
	/// Determines if the current advice applies to the situation.
	/// </summary>
	/// <returns></returns>
	public abstract bool IsAdviceApplicable();
}