using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
	/// <summary>
	/// The text
	/// </summary>
	public Text Text;

	/// <summary>
	/// The canvas group
	/// </summary>
	public CanvasGroup CanvasGroup;

	private void Start()
	{
		HidePopup();
	}

	private void OnEnable()
	{
		EventManager.StartListening<ShowPopupEvent>(OnShowPopup);
		EventManager.StartListening<ClosePopupEvent>(OnClosePopup);
	}

	private void OnDisable()
	{
		EventManager.StopListening<ShowPopupEvent>(OnShowPopup);
		EventManager.StopListening<ClosePopupEvent>(OnClosePopup);
	}

	/// <summary>
	/// Called when [show popup].
	/// </summary>
	/// <param name="event">The event.</param>
	private void OnShowPopup(ShowPopupEvent @event)
	{
		DisplayPopupItem(@event.Item);
	}

	/// <summary>
	/// Called when [close popup].
	/// </summary>
	/// <param name="event">The event.</param>
	private void OnClosePopup(ClosePopupEvent @event)
	{
		HidePopupItems();
	}

	/// <summary>
	/// Displays the popup item.
	/// </summary>
	/// <param name="popupItem">The popup item.</param>
	private void DisplayPopupItem(PopupItem popupItem)
	{
		StartCoroutine(WaitAndDisplay(popupItem));
	}

	/// <summary>
	/// Hides the popup items.
	/// </summary>
	private void HidePopupItems()
	{
		StopAllCoroutines();
		HidePopup();
	}

	/// <summary>
	/// Waits and display the popupitem.
	/// </summary>
	/// <param name="popupItem">The popup item.</param>
	/// <returns></returns>
	private IEnumerator WaitAndDisplay(PopupItem popupItem)
	{
		SetPopupText(popupItem.Text);

		if (popupItem.DelayInSeconds > 0)
		{
			yield return new WaitForSeconds(popupItem.DelayInSeconds);
		}

		ShowPopup();

		// Larger than 0 means we have a limit, else we display indefenitly.
		if (popupItem.DisplayTimeInSeconds > 0)
		{
			yield return new WaitForSeconds(popupItem.DisplayTimeInSeconds);

			HidePopup();
		}
	}

	/// <summary>
	/// Sets the popup text.
	/// </summary>
	/// <param name="text">The text.</param>
	private void SetPopupText(string text)
	{
		Text.text = text;
	}

	/// <summary>
	/// Shows the popup.
	/// </summary>
	private void ShowPopup()
	{
		CanvasGroup.Show();
	}

	/// <summary>
	/// Hides the popup.
	/// </summary>
	private void HidePopup()
	{
		CanvasGroup.Hide();
	}
}
