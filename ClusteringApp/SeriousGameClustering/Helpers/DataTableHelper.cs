using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SeriousGameClustering.Helpers
{
	public static class DataTableHelper
	{
		/// <summary>
		/// Converts the list to data table.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static DataTable ConvertListToDataTable<T>(IEnumerable<T> data)
		{
			DataTable table = new DataTable();
			using (var reader = ObjectReader.Create(data))
			{
				table.Load(reader);
			}
			return table;
		}
	}
}
