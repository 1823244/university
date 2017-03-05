using System;

namespace Program
{
	class GetTime
	{
		public static string SystemTime()
		{
			Kernel32.SystemTime st;
			Kernel32.GetSystemTime(out st);
			return "Сегодня " + FormatDate(st)  + ". Текущее время по UTC: " + FormatTime(st); 
		}

		public static string FormatDate(Kernel32.SystemTime st)
		{
			return String.Format("{0,2}", st.Day.ToString("D2")) + "." + String.Format("{0,2}", st.Month.ToString("D2")) + "." + st.Year;
		}

		public static string FormatTime(Kernel32.SystemTime st)
		{
			return String.Format("{0,2}", st.Hour.ToString("D2")) + ":" + String.Format("{0,2}", st.Minute.ToString("D2")) + ":" + String.Format("{0,2}", st.Second.ToString("D2"));
		}

		public static Kernel32.TimeZoneInformation TimeZone()
		{
			Kernel32.TimeZoneInformation tzi;
			int currentTimeZone = Kernel32.GetTimeZoneInformation(out tzi);

			return tzi;
		}

		public static String Bias()
		{
			Kernel32.TimeZoneInformation t = TimeZone();
			Kernel32.SystemTime st;
			Kernel32.GetSystemTime(out st);
			int utc = (-(t.bias / 60 - (t.standardDate.Month > st.Month && st.Month < t.daylightDate.Month ? 1 : 0)));

			return "Смещение относительно UTC: " + (utc > 0 ? "+" : "") + utc.ToString() +" ч.";
		}

		public static String ShiftingTime(bool isWinter)
		{
			Kernel32.TimeZoneInformation t = TimeZone();
			Kernel32.SystemTime to = isWinter ? t.standardDate : t.daylightDate;
			Kernel32.SystemTime st;
			Kernel32.GetSystemTime(out st);
			if (st.Month > to.Month)
			{
				st.Year++;
			}

			short days = 31;
			Kernel32.SystemTime t1 = new Kernel32.SystemTime(new DateTime(st.Year, to.Month, days));
			while (t1.DayOfWeek != to.DayOfWeek)
			{
				days--;
				t1 = new Kernel32.SystemTime(new DateTime(st.Year, to.Month, days, to.Hour, to.Minute, to.Second));
			}

			return "Переход на " + (isWinter ? "зимнее" : "летнее") + " время: " + FormatDate(t1) + " в " + FormatTime(t1);
		}
	}
}
