using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactions/Advice")]
public class AdviceReaction : Reaction
{
	public AIAdvice[] AdviceToCheck;

	public string NoAdviceText;

	public override void React()
	{
		var builder = new StringBuilder();

		foreach (AIAdvice advice in AdviceToCheck)
		{
			if (advice.IsAdviceApplicable())
			{
				builder.AppendLine(advice.AdviceText);
			}
		}

		string adviceText;

		// Check if any advice was applicable.
		if(builder.Length > 0)
		{
			adviceText = builder.ToString();
		}
		else
		{
			adviceText = NoAdviceText;
		}

		EventManager.TriggerEvent(new ShowPopupEvent(PopupItem.WithDelayAndLimit(adviceText, 0, 10)));

		// Clear advice
		AIManager.ClearEvents();
	}
}
