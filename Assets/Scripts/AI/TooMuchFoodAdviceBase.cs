﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Advice/TooMuchFood")]
public class TooMuchFoodAdviceBase : AIAdvice
{
	public override bool IsAdviceApplicable()
	{
		foreach(SugarHighEvent e in AIManager.SugarHighEvents)
		{
			if(e.Instigator == SugarLevelInstigator.FOOD)
			{
				return true;
			}
		}

		return false;
	}
}