﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	private List<Interaction> Interactions;
	private List<GameObject> ObjectPool;

	private const int INITIAL_OBJECTS = 5;

	private void Awake()
	{
		Interactions = new List<Interaction>();
		ObjectPool = new List<GameObject>();
	}

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

		InitObjectPool();
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
		Interactions.Clear();
		Interactions.AddRange(@event.Interactions);

		UpdateItemList();
	}

	private void OnInteractionRegionExit(EventBase eventBase)
	{
		// There are no items to display anymore
		Interactions.Clear();
		UpdateItemList();
	}

	private void InitObjectPool()
	{
		for (int i = 0; i < INITIAL_OBJECTS; i++)
		{
			GameObject newItem = CreateEmptyInteractionItem();
			ObjectPool.Add(newItem);
		}
	}

	private void UpdateItemList()
	{
		// First de-activate the complete pool
		ObjectPool.ForEach(x => x.SetActive(false));

		foreach (Interaction interaction in Interactions)
		{
			GameObject newItem = GetNextInPool();

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

	private GameObject GetNextInPool()
	{
		GameObject obj = ObjectPool.FirstOrDefault(x => !x.activeInHierarchy);
		if (obj == null)
		{
			obj = CreateEmptyInteractionItem();
			ObjectPool.Add(obj);
		}

		// Ensure it is set to active
		obj.SetActive(true);

		return obj;
	}

	private GameObject CreateEmptyInteractionItem()
	{
		GameObject newItem = Instantiate(InteractionItem);
		newItem.transform.SetParent(ContentPanel.transform);
		newItem.SetActive(false);
		return newItem;
	}
}
