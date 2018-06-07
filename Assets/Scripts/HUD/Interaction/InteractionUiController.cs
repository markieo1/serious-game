using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionUiController : MonoBehaviour
{
	/// <summary>
	/// The interaction item
	/// </summary>
	public GameObject InteractionItem;

	/// <summary>
	/// The content panel
	/// </summary>
	public RectTransform ContentPanel;

	/// <summary>
	/// The canvas group
	/// </summary>
	public CanvasGroup CanvasGroup;

	/// <summary>
	/// Gets a value indicating whether this instance can open.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance can open; otherwise, <c>false</c>.
	/// </value>
	private bool CanOpen
	{
		get
		{
			return Interactions.Any();
		}
	}

	private Interaction[] Interactions;
	private List<GameObject> ObjectPool;

	private const int INITIAL_OBJECTS = 5;

	/// <summary>
	/// Opens this instance.
	/// </summary>
	public void Open()
	{
		if (CanOpen)
		{
			CanvasGroup.alpha = 1;
			CanvasGroup.interactable = true;
			CanvasGroup.blocksRaycasts = true;

			Time.timeScale = 0;
		}
	}

	/// <summary>
	/// Closes this instance.
	/// </summary>
	public void Close()
	{
		CanvasGroup.alpha = 0;
		CanvasGroup.interactable = false;
		CanvasGroup.blocksRaycasts = false;
		Time.timeScale = 1;
	}

	private void Awake()
	{
		Interactions = new Interaction[0];
		ObjectPool = new List<GameObject>(INITIAL_OBJECTS);
	}

	private void Start()
	{
		if (!ContentPanel)
		{
			throw new NotSupportedException("ContentPanel is needed to display interaction items");
		}

		if (!CanvasGroup)
		{
			throw new NotSupportedException("An CanvasGroup is required to show/hide the interaction ui.");
		}

		EventManager.StartListening<EnterInteractionRegionEvent>(OnInteractionRegionEntered);
		EventManager.StartListening<ExitInteractionRegionEvent>(OnInteractionRegionExit);
		EventManager.StartListening<OpenInteractionSelectorEvent>(OnOpenInteractionSelector);

		InitObjectPool();
	}

	private void OnDestroy()
	{
		EventManager.StopListening<EnterInteractionRegionEvent>(OnInteractionRegionEntered);
		EventManager.StopListening<ExitInteractionRegionEvent>(OnInteractionRegionExit);
		EventManager.StopListening<OpenInteractionSelectorEvent>(OnOpenInteractionSelector);
	}

	private void OnInteractionRegionEntered(EnterInteractionRegionEvent @event)
	{
		// There is a possiblity to update the items
		// .Interactions contains the possiblities
		Interactions = @event.Interactions;

		UpdateItemList();
	}

	private void OnInteractionRegionExit(EventBase @event)
	{
		// There are no items to display anymore
		Interactions = new Interaction[0];
		UpdateItemList();

		Close();
	}

	/// <summary>
	/// Initializes the object pool.
	/// </summary>
	private void InitObjectPool()
	{
		for (int i = 0; i < INITIAL_OBJECTS; i++)
		{
			GameObject newItem = CreateEmptyInteractionItem();
			ObjectPool.Add(newItem);
		}
	}

	/// <summary>
	/// Updates the item list.
	/// </summary>
	private void UpdateItemList()
	{
		ObjectPool.ForEach(x => x.SetActive(false));

		foreach (Interaction interaction in Interactions)
		{
			GameObject newItem = GetNextInPool();

			// Get the interactionitem controller
			var itemController = newItem.GetComponent<InteractionItemController>();
			itemController.SetInteraction(interaction);
			itemController.SetUiController(this);
		}
	}

	private void OnOpenInteractionSelector(EventBase eventBase)
	{
		// Check if active, if so we should hide
		bool isOpen = CanvasGroup.alpha > 0;

		if (isOpen)
		{
			Close();
		}
		else
		{
			Open();
		}
	}

	/// <summary>
	/// Gets the next in pool.
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// Creates the empty interaction item.
	/// </summary>
	/// <returns></returns>
	private GameObject CreateEmptyInteractionItem()
	{
		GameObject newItem = Instantiate(InteractionItem);
		newItem.transform.SetParent(ContentPanel.transform);
		newItem.SetActive(false);
		return newItem;
	}
}
