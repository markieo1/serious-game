using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Advice/NotEnoughFood")]
public class NotEnoughFoodAdviceBase : AIAdvice
{
	/// <summary>
	/// The sugar level below which to issue warnings at in the advice.
	/// </summary>
	public float SugarWarningLevel;

	public override bool IsAdviceApplicable()
	{
		foreach(SugarChangedEvent e in AIManager.SugarEvents)
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