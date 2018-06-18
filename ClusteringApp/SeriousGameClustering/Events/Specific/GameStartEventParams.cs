using SeriousGameClustering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriousGameClustering.Events.Specific
{
	public class GameStartEventParams
	{
		/// <summary>
		/// Gets or sets the age.
		/// </summary>
		public int Age { get; set; }

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		public Gender Gender { get; set; }
	}
}