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
	private InteractionUiController UiController;

	void Start()
	{
		if (!ActionText)
		{
			throw new NotSupportedException("Action Text is required for an interaction item, to be able to be displayed.");
		}
	}

	/// <summary>
	/// Sets the interaction.
	/// </summary>
	/// <param name="interaction">The interaction.</param>
	public void SetInteraction(Interaction interaction)
	{
		Interaction = interaction;
		ActionText.text = interaction.Action;
	}

	/// <summary>
	/// Sets the UI controller.
	/// </summary>
	/// <param name="uiController">The UI controller.</param>
	public void SetUiController(InteractionUiController uiController)
	{
		UiController = uiController;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			// We are being touched, lets interact
			Interaction.Interact();

			// Now we must close the ui, since it makes no sense to keep it open
			UiController.Close();

			// Notify the game manager of the interaction
			GameManager.Instance.OnPlayerInteracted();
		}
	}
}
