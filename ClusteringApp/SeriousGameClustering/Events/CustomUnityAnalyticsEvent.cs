using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeriousGameClustering.Events
{
	public abstract class CustomUnityAnalyticsEvent<T> : BaseUnityAnalyticsEvent
	{
		[JsonProperty("custom_params")]
		public T CustomParams { get; set; }
	}
}
