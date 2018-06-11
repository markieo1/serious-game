using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI
{
	public class AIManager
	{
		private static AIManager mInstance;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		public static AIManager Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new AIManager();
				}

				return mInstance;
			}
		}

		private List<SugarChangedEvent> sugarChangeEvents;

		private AIManager()
		{
			this.sugarChangeEvents = new List<SugarChangedEvent>();
			EventManager.StartListening<SugarChangedEvent>(OnSugarChanged);
		}

		private void OnSugarChanged(SugarChangedEvent e)
		{
			// Don't fill up the list with natural decay events
			if(e.Instigator == SugarLevelInstigator.DECAY)
			{
				return;
			}

			this.sugarChangeEvents.Add(e);
		}
	}
}
