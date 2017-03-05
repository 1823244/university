using System;
using System.Runtime.InteropServices;

namespace Program
{
	class Advapi32
	{
		[DllImport("Advapi32")]
		public static extern bool GetUserName(System.Text.StringBuilder sb, ref Int32 length);
	}
}
