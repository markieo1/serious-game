using Accord.MachineLearning;
using Accord.Math;
using McMaster.Extensions.CommandLineUtils;
using SeriousGameClustering.Events.Specific;
using SeriousGameClustering.Helpers;
using SeriousGameClustering.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

			var clusteringModels = from @event in list
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

			// Next we need the datatable
			DataTable clusteringTable = DataTableHelper.ConvertListToDataTable(clusteringModels);

			double[][] jaggedClusteringObservations = clusteringTable.ToJagged();

			// Create a new K-Means algorithm with 3 clusters 
			KMeans kmeans = new KMeans(3);

			// Compute the algorithm, retrieving an integer array
			//  containing the labels for each of the observations
			KMeansClusterCollection clusters = kmeans.Learn(jaggedClusteringObservations);

			// As a result, the first two observations should belong to the
			//  same cluster (thus having the same label). The same should
			//  happen to the next four observations and to the last three.
			int[] labels = clusters.Decide(jaggedClusteringObservations);

			Console.WriteLine("Done");
		}

	}
}
