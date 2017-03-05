using System;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Program
{
    class GetMain
    {
        public static string ComputerName()
        {
			long nSize = 512;
			byte[] buffor = new byte[nSize];
            unsafe 
			{
				long* pSize = &nSize;
				fixed (byte* pBuffor = buffor)
				{
				  Kernel32.GetComputerName(pBuffor, pSize);
				}
			}

			return Encoding.Default.GetString(buffor.Reverse().SkipWhile(x => x == 0).Reverse().ToArray());
        }

		public static string UserName()
		{
			int nSize = 64;
			StringBuilder buffer = new StringBuilder(nSize);
			Advapi32.GetUserName(buffer, ref nSize);

			return buffer.ToString();
		}

		public static string WindowsDirectory()
		{
			int nSize = 100; 
			StringBuilder sb = new StringBuilder(nSize);
			Kernel32.GetWindowsDirectory(sb, Convert.ToUInt32(nSize));

			return sb.ToString();
		}

		public static string SystemDirectory()
		{
			int nSize = 256;
			StringBuilder sb = new StringBuilder(nSize);
			Kernel32.GetSystemDirectory(sb, Convert.ToUInt32(nSize));

			return sb.ToString();
		}

		public static string TempPath()
		{
			int nSize = 256;
			StringBuilder sb = new StringBuilder(nSize);
			Kernel32.GetTempPath(Convert.ToUInt32(nSize), sb);

			return sb.ToString();
		}

		public static string WindowsVersion()
		{
			const uint VER_NT_WORKSTATION = 0x0000001;
			
			Kernel32.OSVersionInfo osvi = new Kernel32.OSVersionInfo();
			osvi.OSVersionInfoSize = (uint)Marshal.SizeOf(osvi);
			
			Kernel32.GetVersionEx(ref osvi);
			
			string OS = "Windows";
			switch (osvi.MajorVersion)
			{
				case 5:
					switch (osvi.MinorVersion)
					{
						case 0:
							OS = "Windows 2000";
							break;
						case 1:
							OS = "Windows XP";
							break;
						case 2:
							OS = "Windows Server 2003";
							break;
					}
					break;
				case 6:
					switch (osvi.MinorVersion)
					{
						case 0:
							if (osvi.ProductType == VER_NT_WORKSTATION)
							{
								OS = "Windows Vista";
							}
							else
							{
								OS = "Windows Server 2008";
							}
							break;
						case 1:
							if (osvi.ProductType == VER_NT_WORKSTATION)
							{
								OS = "Windows 7";
							}
							else
							{
								OS = "Windows Server 2008 R2";
							}
							break;
						case 2:
							if (osvi.ProductType == VER_NT_WORKSTATION)
							{
								OS = "Windows 8";
							}
							else
							{
								OS = "Windows Server 2012";
							}
							break;
						case 3:
							if (osvi.ProductType == VER_NT_WORKSTATION)
							{
								OS = "Windows 8.1";
							}
							else
							{
								OS = "Windows Server 2012 R2";
							}
							break;
					}
					break;
			}
			
			return OS + " " + osvi.MajorVersion + "." + osvi.MinorVersion + "."	+ osvi.BuildNumber + " " + osvi.CSDVersion;
		}
    }
}
