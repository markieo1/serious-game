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
		EventManager.StartListening<OpenInteractionSelectorEvent>(OnOpenInteractionSelector);
	}

	private void OnDestroy()
	{
		EventManager.StopListening<EnterInteractionRegionEvent>(OnInteractionRegionEntered);
		EventManager.StopListening<ExitInteractionRegionEvent>(OnInteractionRegionExit);
		EventManager.StopListening<OpenInteractionSelectorEvent>(OnOpenInteractionSelector);
	}

	private void OnInteractionRegionEntered(EnterInteractionRegionEvent @event)
	{
		PopupItem popupItem = PopupItem.Indefinitely(InfoMessage);
		EventManager.TriggerEvent(new ShowPopupEvent(popupItem));
	}

	private void OnInteractionRegionExit(EventBase @event)
	{
		EventManager.TriggerEvent(new ClosePopupEvent());
	}

	private void OnOpenInteractionSelector(EventBase eventBase)
	{
		EventManager.TriggerEvent(new ClosePopupEvent());
	}
}
