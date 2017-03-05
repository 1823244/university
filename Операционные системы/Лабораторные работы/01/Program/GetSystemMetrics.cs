namespace Program
{
	class GetMetrics
	{
		public static string Metric(int number)
		{
			return User32.GetSystemMetrics(number).ToString();
		}
	}
}
