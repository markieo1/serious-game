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

		private AIManager()
		{
			EventManager.StartListening<SugarChangedEvent>(OnSugarChanged);
		}

		private void OnSugarChanged(SugarChangedEvent e)
		{
			
		}
	}
}
