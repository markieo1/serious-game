using SeriousGameClustering.Events;
using SeriousGameClustering.Events.Specific;
using SeriousGameClustering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriousGameClustering.Helpers
{
	public static class ClusteringHelper
	{
		/// <summary>
		/// Converts to clustering model.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static IEnumerable<ClusteringModel> ConvertToClusteringModel(IEnumerable<BaseUnityAnalyticsEvent> data)
		{
			return from @event in data
				   group @event by @event.SessionId into eventGroup
				   select new ClusteringModel
				   {
					   AmountOfSugarLow = eventGroup.LongCount(x => x.Name == "SugarLow"),
					   AmountOfSugarHigh = eventGroup.LongCount(x => x.Name == "SugarHigh"),
					   MostOccuredHighInstigator = (from sugarEvent in eventGroup
													where sugarEvent.Name == "SugarHigh" && sugarEvent.GetType() == typeof(SugarEvent)
													let sugarLowEvent = sugarEvent as SugarEvent
													group sugarLowEvent by sugarLowEvent.CustomParams.Instigator into instigatorGroup
													orderby instigatorGroup.LongCount() descending
													select instigatorGroup.Key).FirstOrDefault(),
					   MostOccuredLowInstigator = (from sugarEvent in eventGroup
												   where sugarEvent.Name == "SugarLow" && sugarEvent.GetType() == typeof(SugarEvent)
												   let sugarLowEvent = sugarEvent as SugarEvent
												   group sugarLowEvent by sugarLowEvent.CustomParams.Instigator into instigatorGroup
												   orderby instigatorGroup.LongCount() descending
												   select instigatorGroup.Key).FirstOrDefault(),
					   DistanceTraveled = (from gameEvent in eventGroup
										   where gameEvent.Name == "game_over" && gameEvent.GetType() == typeof(GameOverEvent)
										   let gameOverEvent = gameEvent as GameOverEvent
										   select gameOverEvent.CustomParams?.DistanceTravelled ?? 0).DefaultIfEmpty().Max(),
					   Age = 0 //eventGroup.Where(x => x.Name == "game_start").FirstOrDefault().Name;

				   };

		}
	}
}
