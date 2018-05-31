using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionItemController : MonoBehaviour
{
	public Text Text;

	void Start()
	{
		if (!Text)
		{
			throw new NotSupportedException("Text is required for an interaction item, to be able to be displayed.");
		}
	}

	public void SetAction(string newAction)
	{
		Text.text = newAction;
	}
}
