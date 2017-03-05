using System.Windows.Media;

namespace Program
{
	class GetColors
	{
		public static Color Element(int n)
		{
			uint value = User32.GetSysColor(n);
			return Color.FromArgb((byte)((value >> 24) & 0xFF), (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF), (byte)((value >> 16) & 0xFF));
		}
	}
}
