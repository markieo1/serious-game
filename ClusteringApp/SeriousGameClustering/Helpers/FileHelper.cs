using Newtonsoft.Json;
using SeriousGameClustering.Events;
using SeriousGameClustering.Events.Specific;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace SeriousGameClustering.Helpers
{
	public static class FileHelper
	{
		public static List<BaseUnityAnalyticsEvent> ReadAndConvert(string fileLocation)
		{
			Dictionary<string, Type> map = new Dictionary<string, Type>()
			{
				{ "SugarLow", typeof(SugarEvent) },
				{ "game_over", typeof(GameOverEvent) },
				{ "game_start", typeof(GameStartEvent) }
			};

			List<BaseUnityAnalyticsEvent> results = new List<BaseUnityAnalyticsEvent>();

			string[] lines = File.ReadAllLines(fileLocation);

			// Loop through all the lines and deserialize
			foreach (string line in lines)
			{
				var analyticsEvent = JsonConvert.DeserializeObject<BaseUnityAnalyticsEvent>(line);

				if (analyticsEvent.Type != "custom")
				{
					continue;
				}
				// Correct to the correct instance
				if (!map.ContainsKey(analyticsEvent.Name))
				{
					continue;
				}

				var result = JsonConvert.DeserializeObject(line, map[analyticsEvent.Name]) as BaseUnityAnalyticsEvent;
				results.Add(result);
			}

			return results;
		}
	}
}
