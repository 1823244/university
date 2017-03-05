using System.Windows.Media;

namespace Program
{
	class SetColors
	{
		public static void Elements(int n, int[] els, Color[] c)
		{
			int[] colors = new int[3];
			for (int i = 0; i < c.Length; i++)
			{
				colors[i] = (int)((c[i].B << 16) | (c[i].G << 8)  | (c[i].R << 0)) & 0x00FFFFFF;
			}

			User32.SetSysColors(n, els, colors);
		}
	}
}
