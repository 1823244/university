using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Program
{
	class Kernel32
	{
		[DllImport("Kernel32")]
		public static extern unsafe bool GetComputerName(byte* lpBuffer, long* nSize);

		[DllImport("Kernel32")]
		public static extern uint GetWindowsDirectory(StringBuilder lpBuffer, uint uSize);

		[DllImport("Kernel32")]
		public static extern uint GetSystemDirectory([Out] StringBuilder lpBuffer, uint uSize);

		[DllImport("Kernel32")]
		public static extern uint GetTempPath(uint nBufferLength, [Out] StringBuilder lpBuffer);

		[DllImport("Kernel32")]
		public static extern bool GetVersionEx(ref OSVersionInfo info);

		public struct OSVersionInfo
		{
			public uint OSVersionInfoSize;
			public uint MajorVersion;
			public uint MinorVersion;
			public uint BuildNumber;
			public uint PlatformId;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string CSDVersion;
			public Int16 ServicePackMajor;
			public Int16 ServicePackMinor;
			public Int16 SuiteMask;
			public Byte ProductType;
			public Byte Reserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SystemTime 
		{
			[MarshalAs(UnmanagedType.U2)]
			public short Year;
			[MarshalAs(UnmanagedType.U2)]
			public short Month;
			[MarshalAs(UnmanagedType.U2)]
			public short DayOfWeek;
			[MarshalAs(UnmanagedType.U2)]
			public short Day;
			[MarshalAs(UnmanagedType.U2)]
			public short Hour;
			[MarshalAs(UnmanagedType.U2)]
			public short Minute;
			[MarshalAs(UnmanagedType.U2)]
			public short Second;
			[MarshalAs(UnmanagedType.U2)]
			public short Milliseconds;

			public SystemTime(DateTime dt)
			{
				dt = dt.ToUniversalTime();
				Year = (short)dt.Year;
				Month = (short)dt.Month;
				DayOfWeek = (short)dt.DayOfWeek;
				Day = (short)dt.Day;
				Hour = (short)dt.Hour;
				Minute = (short)dt.Minute;
				Second = (short)dt.Second;
				Milliseconds = (short)dt.Millisecond;
			}
		}

		[DllImport("Kernel32")]
		public static extern void GetSystemTime(out SystemTime lpSystemTime);

		[DllImport("Kernel32", CharSet = CharSet.Auto)]
		public static extern int GetTimeZoneInformation(out TimeZoneInformation lpTimeZoneInformation);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct TimeZoneInformation
		{
			public int bias;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string standardName;
			public SystemTime standardDate;
			public int standardBias;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string daylightName;
			public SystemTime daylightDate;
			public int daylightBias;
		}

		[DllImport("Kernel32", SetLastError = true)]
		public static extern int EnumCalendarInfo(int lpCalInfoEnumProc, int Locale, int Calendar, int CalType);

		[DllImport("Kernel32")]
		public static extern int GetCurrencyFormat(uint Locale, uint dwFlags, string lpValue, CurrencyFormat lpFormat, IntPtr lpCurrencyStr, int cchCurrency);

		[StructLayout(LayoutKind.Sequential)]
		public class CurrencyFormat
		{
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 uiNumDigits;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 uiLeadingZero;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 uiGrouping;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String lpDecimalSep;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String lpThousandSep;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 uiNegativeOrder;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 uiPositiveOrder;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String lpCurrencySymbol;

			public CurrencyFormat(uint nd, uint lz, uint g, string ds, string ts, uint no, uint po, string cs)
			{
				uiNumDigits = nd;
				uiLeadingZero = lz;
				uiGrouping = g;
				lpDecimalSep = ds;
				lpThousandSep = ts;
				uiNegativeOrder = no;
				uiPositiveOrder = po;
				lpCurrencySymbol = cs;
			}
		};

		[DllImport("Kernel32")]
		public static extern uint GetLastError();
	}
}
