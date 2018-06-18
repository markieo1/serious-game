using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeriousGameClustering.Events
{
	public class BaseUnityAnalyticsEvent
	{
		/// <summary>
		/// The timestamp(in milliseconds) at which the event was generated on the device.Note that device-generated timestamps can be skewed due to the device clock and latency in receiving the event 
		/// </summary>
		[JsonProperty("ts")]
		public long TimeStamp { get; set; }

		/// <summary>
		/// The type of event being queried (i.e.Custom, DeviceInfo, Transaction, etc)
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// The name of the Custom Event (e.g. “LevelComplete”)	
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Unity Analytics generated identifier for the session. If a game is reopened after more than 30 minutes of inactivity, a new session ID is generated	
		/// </summary>
		public long SessionId { get; set; }
	}
}
