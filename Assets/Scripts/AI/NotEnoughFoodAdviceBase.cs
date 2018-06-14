using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Advice/NotEnoughFood")]
public class NotEnoughFoodAdviceBase : AIAdvice
{
	public override bool IsAdviceApplicable()
	{
		foreach(SugarLowEvent e in AIManager.SugarLowEvents)
		{
			if(e.Instigator == SugarLevelInstigator.DECAY)
			{
				return true;
			}
		}

		return false;
	}
}