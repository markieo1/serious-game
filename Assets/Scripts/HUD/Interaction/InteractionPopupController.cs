using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPopupController : MonoBehaviour
{
	/// <summary>
	/// The information message
	/// </summary>
	public string InfoMessage = "Press the \"Interact\" button to open the interaction menu.";

	// Use this for initialization
	void Start()
	{
		EventManager.StartListening<EnterInteractionRegionEvent>(OnInteractionRegionEntered);
		EventManager.StartListening<ExitInteractionRegionEvent>(OnInteractionRegionExit);
		EventManager.StartListening<InteractionSelectorChangeEvent>(OnInteractionSelectorChange);
	}

	private void OnDestroy()
	{
		EventManager.StopListening<EnterInteractionRegionEvent>(OnInteractionRegionEntered);
		EventManager.StopListening<ExitInteractionRegionEvent>(OnInteractionRegionExit);
		EventManager.StopListening<InteractionSelectorChangeEvent>(OnInteractionSelectorChange);
	}

	private void OnInteractionRegionEntered(EnterInteractionRegionEvent @event)
	{
		if (GameManager.Instance.IsPaused) return;

		PopupItem popupItem = PopupItem.Indefinitely(InfoMessage);
		EventManager.TriggerEvent(new ShowPopupEvent(popupItem));
	}

	private void OnInteractionRegionExit(EventBase @event)
	{
		EventManager.TriggerEvent(new ClosePopupEvent());
	}

	private void OnInteractionSelectorChange(InteractionSelectorChangeEvent @event)
	{
		if (GameManager.Instance.IsPaused) return;

		// Check if are not opening the interaction selector
		if (!@event.ShouldOpen) return;

		EventManager.TriggerEvent(new ClosePopupEvent());
	}
}
