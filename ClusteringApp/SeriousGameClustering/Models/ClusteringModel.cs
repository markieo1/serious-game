using SeriousGameClustering.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeriousGameClustering.Models
{
	/// <summary>
	/// Unique per player
	/// </summary>
	public class ClusteringModel
	{
		public long AmountOfSugarLow { get; set; }
		public long AmountOfSugarHigh { get; set; }
		public Instigator MostOccuredLowInstigator { get; set; }
		public Instigator MostOccuredHighInstigator { get; set; }
		public Gender Gender { get; set; }
		public int Age { get; set; }
		public double DistanceTraveled { get; set; }
	}
}
