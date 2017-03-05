using System.Collections.Generic;
using System.Data;

namespace Project
{
	public static class ExtensionClass
	{
		public static string Type(this DataTable dt, int index)
		{
			return dt.Rows[index].ItemArray[0].ToString();
		}

		public static string Value(this DataTable dt, int index)
		{
			return dt.Rows[index].ItemArray[1].ToString();
		}

		public static int RCount(this DataTable dt)
		{
			return dt.Rows.Count;
		}

		public static bool IsEmpty(this Stack<string> s)
		{
			return s.Count == 0;
		}
	}
}
