using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionItemController : MonoBehaviour, IPointerClickHandler
{
	public Text ActionText;

	private Interaction Interaction;

	void Start()
	{
		if (!ActionText)
		{
			throw new NotSupportedException("Action Text is required for an interaction item, to be able to be displayed.");
		}
	}

	public void SetInteraction(Interaction interaction)
	{
		Interaction = interaction;
		ActionText.text = interaction.Action;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			// We are being touched, lets interact
			Interaction.Interact();
		}
	}
}
