using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
	/// <summary>
	/// The interaction item
	/// </summary>
	public GameObject InteractionItem;

	/// <summary>
	/// The content panel
	/// </summary>
	public RectTransform ContentPanel;

	private List<Interaction> Interactions = new List<Interaction>();

	// Use this for initialization
	void Start()
	{
		EventManager.StartListening(EventsTypes.EnterInteractionRegion, OnInteractionRegionEntered);
		EventManager.StartListening(EventsTypes.ExitInteractionRegion, OnInteractionRegionExit);

		if (!ContentPanel)
		{
			throw new NotSupportedException("RectTransform of the ScrollView not found or children game objects");
		}

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
			itemController.SetAction(interaction.Action);
		}
	}
}
