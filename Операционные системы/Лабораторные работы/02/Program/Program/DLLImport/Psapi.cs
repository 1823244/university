using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
	class Psapi
	{
		/// <summary>
		/// Retrieves the process identifier for each process object in the system
		/// </summary>
		/// <param name="processIds">A pointer to an array that receives the list of process identifiers</param>
		/// <param name="arraySizeBytes">The size of the pProcessIds array, in bytes</param>
		/// <param name="bytesCopied">The number of bytes returned in the pProcessIds</param>
		/// <returns>Nonzero if succeeds, else 0</returns>
		[DllImport("Psapi", SetLastError = true)]
		public static extern bool EnumProcesses([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)]
												[In][Out] UInt32[] processIds, UInt32 arraySizeBytes, 
												[MarshalAs(UnmanagedType.U4)] out UInt32 bytesCopied);

		/// <summary>
		/// Retrieves a handle for each module in the specified process
		/// </summary>
		/// <param name="hProcess">A handle to the process</param>
		/// <param name="lphModule">An array that receives the list of module handles</param>
		/// <param name="cb">The size of the lphModule array, in bytes</param>
		/// <param name="lpcbNeeded">The number of bytes required to store all module handles in the lphModule</param>
		/// <returns>Nonzero if succeeds, else 0</returns>
		[DllImport("Psapi", SetLastError = true)]
		public static extern bool EnumProcessModules(IntPtr hProcess,
													[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] 
													[In][Out] uint[] lphModule, uint cb,
													[MarshalAs(UnmanagedType.U4)] out uint lpcbNeeded);

		/// <summary>
		/// Retrieves the base name of the specified module
		/// </summary>
		/// <param name="hProcess">A handle to the process that contains the module</param>
		/// <param name="hModule">A handle to the module</param>
		/// <param name="lpBaseName">A pointer to the buffer that receives the base name of the module</param>
		/// <param name="nSize">The size of the lpBaseName buffer, in characters</param>
		/// <returns>0 if fails, else value specifies the length of the string copied to the buffer</returns>
		[DllImport("Psapi")]
		public static extern uint GetModuleBaseName(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName,
													uint nSize);

		/// <summary>
		/// Retrieves the fully qualified path for the file containing the specified module
		/// </summary>
		/// <param name="hProcess">A handle to the process that contains the module</param>
		/// <param name="hModule">A handle to the module</param>
		/// <param name="lpBaseName">A pointer to a buffer that receives the fully qualified path to the module</param>
		/// <param name="nSize">The size of the lpFilename buffer, in characters.</param>
		/// <returns>0 if fails, else value specifies the length of the string copied to the buffer</returns>
		[DllImport("Psapi")]
		public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName,
													  [In] [MarshalAs(UnmanagedType.U4)] int nSize);

		/// <summary>
		/// Contains the module load address, size, and entry point
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct MODULEINFO
		{
			/// <summary>
			/// The load address of the module
			/// </summary>
			public IntPtr lpBaseOfDll;
			/// <summary>
			/// The size of the linear space that the module occupies, in bytes
			/// </summary>
			public uint SizeOfImage;
			/// <summary>
			/// The entry point of the module
			/// </summary>
			public IntPtr EntryPoint;
		}

		/// <summary>
		/// Retrieves information about the specified module in the MODULEINFO structure
		/// </summary>
		/// <param name="hProcess">A handle to the process that contains the module</param>
		/// <param name="hModule">A handle to the module</param>
		/// <param name="lpmodinfo">A pointer to the MODULEINFO structure</param>
		/// <param name="cb">The size of the MODULEINFO structure, in bytes</param>
		/// <returns>Nonzero if succeeds, else 0</returns>
		[DllImport("Psapi", SetLastError = true)]
		public static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, out MODULEINFO lpmodinfo,
													   uint cb);

		/// <summary>
		/// Retrieves the load address for each device driver in the system
		/// </summary>
		/// <param name="ddAddresses">An array that receives the list of load addresses for the device drivers</param>
		/// <param name="arraySizeBytes">The size of the lpImageBase array, in bytes</param>
		/// <param name="bytesNeeded">The number of bytes returned in the lpImageBase array</param>
		/// <returns>Nonzero if succeeds, else 0</returns>
		[DllImport("Psapi")]
		public static extern bool EnumDeviceDrivers([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)]
													[In][Out] UInt32[] ddAddresses, UInt32 arraySizeBytes,
													[MarshalAs(UnmanagedType.U4)] out UInt32 bytesNeeded);

		/// <summary>
		/// Retrieves the base name of the specified device driver
		/// </summary>
		/// <param name="ddAddress">The load address of the device driver</param>
		/// <param name="ddBaseName">A pointer to the buffer that receives the base name of the device driver</param>
		/// <param name="baseNameStringSizeChars">The size of the lpBaseName buffer, in characters</param>
		/// <returns>0 if failes, else value specifies the length of the string in buffer, not including '\0'</returns>
		[DllImport("Psapi")]
		public static extern int GetDeviceDriverBaseName(UInt32 ddAddress, StringBuilder ddBaseName, 
														  int baseNameStringSizeChars);
	}
}
