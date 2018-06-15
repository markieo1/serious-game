using SeriousGameClustering.Events.Specific;
using System;
using System.Linq;

namespace SeriousGameClustering
{
	class Program
	{
		static void Main(string[] args)
		{
			var list = FileHelper.ReadAndConvert();

			var t = from @event in list
					group @event by @event.UserId into eventGroup
					select new ClusteringModel
					{
						AmountOfSugarLow = eventGroup.LongCount(x => x.Name == "SugarLow"),
						AmountOfSugarHigh = eventGroup.LongCount(x => x.Name == "SugarHigh"),
						MostOccuredHighInstigator = (from sugarEvent in eventGroup
													 where sugarEvent.Name == "SugarHigh" && sugarEvent.GetType() == typeof(SugarEvent)
													 let sugarLowEvent = sugarEvent as SugarEvent
													 group sugarLowEvent by sugarLowEvent.CustomParams.Instigator into instigatorGroup
													 orderby instigatorGroup.LongCount() descending
													 select instigatorGroup.Key).First(),
						MostOccuredLowInstigator = (from sugarEvent in eventGroup
													where sugarEvent.Name == "SugarLow" && sugarEvent.GetType() == typeof(SugarEvent)
													let sugarLowEvent = sugarEvent as SugarEvent
													group sugarLowEvent by sugarLowEvent.CustomParams.Instigator into instigatorGroup
													orderby instigatorGroup.LongCount() descending
													select instigatorGroup.Key).First(),
						



						Age = 0 //eventGroup.Where(x => x.Name == "game_start").FirstOrDefault().Name;

					};


			Console.WriteLine("Hello World!");
		}
	}
}
