using System;
using System.Collections.Generic;
using System.Text;

namespace SeriousGameClustering.Events
{
	public abstract class CustomUnityAnalyticsEvent<T> : BaseUnityAnalyticsEvent
	{
		public T CustomParams { get; set; }
	}
}
