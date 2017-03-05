using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Program
{
	class GetAdditional
	{
		public static string CurrencyFormat(double numberToConvert, uint nd, uint g, string ds, string ts)
		{
			string sCurrency = null;
			long lRet = 0;
			Kernel32.CurrencyFormat cf = new Kernel32.CurrencyFormat(nd, 1, g, ds, ts, 2, 2, "");

			lRet = Kernel32.GetCurrencyFormat(0, 0, numberToConvert.ToString(), cf, IntPtr.Zero, 0);

			if (0 != lRet)
			{
				IntPtr ptrCurrencyStr = Marshal.AllocHGlobal((int)lRet);
				lRet = Kernel32.GetCurrencyFormat(0, 0, numberToConvert.ToString(), cf, ptrCurrencyStr, (int)lRet);
				sCurrency = 0 == lRet ? null : Marshal.PtrToStringAnsi(ptrCurrencyStr);
				Marshal.FreeHGlobal(ptrCurrencyStr);

				return sCurrency;
			}
			else
			{
				return "Ошибка!";
			}
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool SetVolumeLabel(string lpRootPathName, string lpVolumeName);

		public static uint LastError()
		{
			SetVolumeLabel("XYZ:\\", "My Imaginary Drive");
			return Kernel32.GetLastError();
		}

		public static string OemToChar(string str)
		{
			if (string.IsNullOrEmpty(str)) return str;
			
			StringBuilder strBuilder = new StringBuilder(str.Length);
			User32.OemToChar(str.ToString(), strBuilder);
			
			return strBuilder.ToString();
		}

		public static string CharToOem(string str)
		{
			if (string.IsNullOrEmpty(str)) return str;

			StringBuilder strBuilder = new StringBuilder(str.Length);
			User32.CharToOem(str.ToString(), strBuilder);

			return strBuilder.ToString();
		}
	}
}
