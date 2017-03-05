using System;
using System.Runtime.InteropServices;

namespace Program
{
	class GetParameters
	{
		public struct AccessTimeOut
		{
			public int cbSize;
			public int dwFlags;
			public int TimeOutMSec;
		}

		public static string Parameter(int number)
		{
			AccessTimeOut ato = new AccessTimeOut();
			int size = ato.cbSize = Marshal.SizeOf(typeof(AccessTimeOut));

			IntPtr metrics = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(ato, metrics, true);

			bool b = User32.SystemParametersInfo(number, size, metrics, 0);
			AccessTimeOut atoin = (AccessTimeOut)Marshal.PtrToStructure(metrics, typeof(AccessTimeOut));
			Marshal.FreeHGlobal(metrics);

			return atoin.TimeOutMSec.ToString();
		}

		public static MainWindow.FilterKeyStruct FilterKey()
		{
			MainWindow.FilterKeyStruct startupFilterKeys = new MainWindow.FilterKeyStruct();
			int size = startupFilterKeys.cbSize = Marshal.SizeOf(typeof(MainWindow.FilterKeyStruct));

			IntPtr metrics = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(startupFilterKeys, metrics, true);

			bool b = User32.SystemParametersInfo(50, size, metrics, 0);
			MainWindow.FilterKeyStruct result = (MainWindow.FilterKeyStruct)Marshal.PtrToStructure(metrics, typeof(MainWindow.FilterKeyStruct));
			Marshal.FreeHGlobal(metrics);

			return result;
		}

		public static MainWindow.MinimizedMetricsStruct MinimizedMetrics()
		{
			MainWindow.MinimizedMetricsStruct mm = new MainWindow.MinimizedMetricsStruct();
			int size = mm.cbSize = Marshal.SizeOf(typeof(MainWindow.MinimizedMetricsStruct));

			IntPtr metrics = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(mm, metrics, true);

			bool b = User32.SystemParametersInfo(43, size, metrics, 0);
			MainWindow.MinimizedMetricsStruct result = (MainWindow.MinimizedMetricsStruct)Marshal.PtrToStructure(metrics, typeof(MainWindow.MinimizedMetricsStruct));
			Marshal.FreeHGlobal(metrics);

			return result;
		}
	}
}
