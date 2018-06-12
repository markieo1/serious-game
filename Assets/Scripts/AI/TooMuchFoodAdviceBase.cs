using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Advice/TooMuchFood")]
public class TooMuchFoodAdviceBase : AIAdvice
{
	/// <summary>
	/// The sugar level to issue warnings at in the advice.
	/// </summary>
	public float SugarWarningLevel;

	public override bool IsAdviceApplicable()
	{
		foreach(SugarChangedEvent e in AIManager.SugarEvents.Where(ev => ev.Instigator == SugarLevelInstigator.FOOD))
		{
			float change = e.Value - e.OldValue;
			if(change > 0 && e.Value > SugarWarningLevel)
			{
				return true;
			}
		}

		return false;
	}
}