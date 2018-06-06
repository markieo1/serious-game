using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShowPopupEvent : EventBase
{
	/// <summary>
	/// Gets or sets the item.
	/// </summary>
	public PopupItem Item { get; set; }

	public ShowPopupEvent(PopupItem item)
	{
		Item = item;
	}
}

