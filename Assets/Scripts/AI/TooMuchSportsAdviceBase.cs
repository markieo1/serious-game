using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Advice/TooMuchSports")]
public class TooMuchSportsAdviceBase : AIAdvice
{
	public override bool IsAdviceApplicable()
	{
		foreach(SugarLowEvent e in AIManager.SugarLowEvents)
		{
			if(e.Instigator == SugarLevelInstigator.EXERCISE)
			{
				return true;
			}
		}

		return false;
	}
}
