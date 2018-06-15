using System;
using System.Collections.Generic;
using System.Text;

namespace SeriousGameClustering.Events.Specific
{
	public class SugarEventParams
	{
		public double Value { get; set; }
		public double OldValue { get; set; }
		public Instigator Instigator { get; set; }
	}
}
