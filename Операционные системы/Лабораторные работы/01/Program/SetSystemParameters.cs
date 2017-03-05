using System;
using System.Runtime.InteropServices;

namespace Program
{
	class SetParameters
	{
		public static void FilterKey(MainWindow.FilterKeyStruct fk)
		{
			int size = fk.cbSize;

			IntPtr metrics = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(fk, metrics, true);

			User32.SystemParametersInfo(51, size, metrics, 0);
		}

		public static void MinimizedMetrics(MainWindow.MinimizedMetricsStruct mm)
		{
			int size = mm.cbSize;

			IntPtr metrics = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(mm, metrics, true);

			User32.SystemParametersInfo(44, size, metrics, 0);
		}
	}
}
