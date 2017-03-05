using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Program
{
	class User32
	{
		[DllImport("User32")]
		public static extern int GetSystemMetrics(int smIndex);

		[DllImport("User32", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int uiAction, int uiParam, IntPtr pvParam, int fuWinIni);

		[DllImport("User32")]
		public static extern uint GetSysColor(int nIndex);

		[DllImport("User32")]
		public static extern int SetSysColors(int cElements, Int32[] lpaElements, Int32[] lpaRgbValues);

		[DllImport("User32", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr ActivateKeyboardLayout(int hkl, uint uFlags);

		[DllImport("User32")]
		public static extern bool OemToChar(string lpszSrc, [Out] StringBuilder lpszDst);

		[DllImport("User32")]
		public static extern bool CharToOem(string lpszSrc, [Out] StringBuilder lpszDst);
	}
}
