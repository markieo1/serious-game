using McMaster.Extensions.CommandLineUtils;
using SeriousGameClustering.Events.Specific;
using System;
using System.Linq;

namespace SeriousGameClustering
{
	class Program
	{
		static void Main(string[] args)
		{
			var app = new CommandLineApplication
			{
				Name = "ClusteringApp",
				Description = ".NET Core console app for handling clustering."
			};
			app.HelpOption("-?|-h|--help");

			var optionFilePath = app.Option("--path <PATH>", "Path to read data from", CommandOptionType.SingleValue)
									.IsRequired()
									.Accepts(v => v.ExistingFile());

			app.OnExecute(() =>
			{
				string filePath = optionFilePath.Value();

				MainClustering(filePath);
			});

			app.Execute(args);
		}

		private static void MainClustering(string filepath)
		{
			var list = FileHelper.ReadAndConvert(filepath);

			var t = from @event in list
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

			var te = t.ToList();


			Console.WriteLine("Hello World!");
		}
	}
}
