using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUiController : MonoBehaviour
{
	/// <summary>
	/// The interaction UI
	/// </summary>
	public GameObject InteractionUI;

	/// <summary>
	/// The interaction item
	/// </summary>
	public GameObject InteractionItem;

	/// <summary>
	/// The content panel
	/// </summary>
	public RectTransform ContentPanel;

	private List<Interaction> Interactions = new List<Interaction>();

	private void Start()
	{
		if (!InteractionUI)
		{
			throw new NotSupportedException("InteractionUI is needed to display interaction items");
		}

		if (!ContentPanel)
		{
			throw new NotSupportedException("ContentPanel is needed to display interaction items");
		}

		EventManager.StartListening(EventsTypes.EnterInteractionRegion, OnInteractionRegionEntered);
		EventManager.StartListening(EventsTypes.ExitInteractionRegion, OnInteractionRegionExit);
		EventManager.StartListening(EventsTypes.OpenInteractionSelector, OnOpenInteractionSelector);

		for (int i = 0; i < 10; i++)
		{
			Interactions.Add(new Interaction()
			{
				Action = "Test" + i
			});

		}

		AddItems();
	}

	private void OnDestroy()
	{
		EventManager.StopListening(EventsTypes.EnterInteractionRegion, OnInteractionRegionEntered);
		EventManager.StopListening(EventsTypes.ExitInteractionRegion, OnInteractionRegionExit);
		EventManager.StopListening(EventsTypes.OpenInteractionSelector, OnOpenInteractionSelector);
	}

	private void OnInteractionRegionEntered(EventBase eventBase)
	{
		// There is a possiblity to update the items
		EnterInteractionRegionEvent @event = eventBase as EnterInteractionRegionEvent;

		// .Interactions contains the possiblities

	}

	private void OnInteractionRegionExit(EventBase eventBase)
	{
		// There are no items to display anymore
	}

	private void AddItems()
	{
		foreach (Interaction interaction in Interactions)
		{
			GameObject newItem = Instantiate(InteractionItem);
			newItem.transform.SetParent(ContentPanel.transform);

			// Get the interactionitem controller
			var itemController = newItem.GetComponent<InteractionItemController>();
			itemController.SetInteraction(interaction);
		}
	}


	private void OnOpenInteractionSelector(EventBase eventBase)
	{
		// Check if active, if so we should hide
		bool isActive = InteractionUI.activeInHierarchy;
		InteractionUI.SetActive(!isActive);
	}
}
