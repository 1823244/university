using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
	class Kernel32
	{
		/// <summary>
		/// Returns full name of running file
		/// </summary>
		/// <param name="hModule">A handle of module</param>
		/// <param name="lpFilename">The pointer to buffer of path to module</param>
		/// <param name="nSize">Length of buffer</param>
		/// <returns>0 if failed, else length of path to module</returns>
		[DllImport("Kernel32", SetLastError = true)]
		[PreserveSig]
		public static extern uint GetModuleFileName([In] IntPtr hModule, [Out] StringBuilder lpFilename,
													[In] [MarshalAs(UnmanagedType.U4)] int nSize);

		/// <summary>
		/// Returns handle of module by filename
		/// </summary>
		/// <param name="lpModuleName">Filename</param>
		/// <returns>NULL if failed, else a handle to the specified module</returns>
		[DllImport("Kernel32", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		/// <summary>
		/// Defines ID of the calling process
		/// </summary>
		/// <returns>The process identifier of the calling process</returns>
		[DllImport("Kernel32")]
		public static extern uint GetCurrentProcessId();
		
		/// <summary>
		/// Defines pseudo handle to the current process.
		/// </summary>
		/// <returns>The pseudo handle to the current process.</returns>
		[DllImport("Kernel32")]
		public static extern IntPtr GetCurrentProcess();

		/// <summary>
		/// Duplicates an object handle
		/// </summary>
		/// <param name="hSourceProcessHandle">A handle to the process with the handle to be duplicated</param>
		/// <param name="hSourceHandle">The handle to be duplicated</param>
		/// <param name="hTargetProcessHandle">A handle to the process that is to receive the duplicated handle</param>
		/// <param name="lpTargetHandle">A pointer to a variable that receives the duplicate handle</param>
		/// <param name="dwDesiredAccess">The access requested for the new handle</param>
		/// <param name="bInheritHandle">A variable that indicates whether the handle is inheritable</param>
		/// <param name="dwOptions">Optional actions. 0x00000000 is nothing, 
		///											  0x00000001 closes the source handle,
		///											  0x00000002 ignores the dwDesiredAccess parameter</param>
		/// <returns>Nonzero if succeeds, else 0</returns>
		[DllImport("Kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, 
												  IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, 
												  uint dwDesiredAccess, 
												  [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

		/// <summary>
		/// Opens an existing local process object
		/// </summary>
		/// <param name="dwDesiredAccess">The access to the process object</param>
		/// <param name="bInheritHandle">If this value is TRUE, child-processes inherit the handle</param>
		/// <param name="dwProcessId">The identifier of the local process to be opened</param>
		/// <returns>If succeeds an open handle to the specified process, else NULL</returns>
		[DllImport("Kernel32")]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		[Flags]
		public enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VMOperation = 0x00000008,
			VMRead = 0x00000010,
			VMWrite = 0x00000020,
			DupHandle = 0x00000040,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			Synchronize = 0x00100000
		}

		/// <summary>
		/// Closes an open object handle
		/// </summary>
		/// <param name="hObject">A valid handle to an open object</param>
		/// <returns>Nonzero if succeeds, else 0</returns>
		[DllImport("Kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr hObject);

		/// <summary>
		/// Takes a snapshot of the specified processes
		/// </summary>
		/// <param name="dwFlags">The portions of the system to be included in the snapshot</param>
		/// <param name="th32ProcessID">The process identifier of the process to be included in the snapshot</param>
		/// <returns>If succeeds an open handle to the specified snapshot, else INVALID_HANDLE_VALUE</returns>
		[DllImport("Kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern IntPtr CreateToolhelp32Snapshot([In]UInt32 dwFlags, [In]UInt32 th32ProcessID);

		[Flags]
		public enum SnapshotFlags : uint
		{
			HeapList = 0x00000001,
			Process = 0x00000002,
			Thread = 0x00000004,
			Module = 0x00000008,
			Module32 = 0x00000010,
			Inherit = 0x80000000,
			All = 0x0000001F,
			NoHeaps = 0x40000000
		}

		/// <summary>
		/// Struct for Process32First
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct PROCESSENTRY32
		{
			/// <summary>
			/// The size of the structure, in bytes
			/// </summary>
			internal UInt32 dwSize;

			/// <summary>
			/// This member is no longer used and is always set to zero. But this is counter of processes's refs
			/// </summary>
			internal UInt32 cntUsage;
           
			/// <summary>
			/// The process identifier
			/// </summary>
			internal UInt32 th32ProcessID;

			/// <summary>
			/// This member is no longer used and is always set to zero. But this is heal ID
			/// </summary>
			internal IntPtr th32DefaultHeapID;
  
			/// <summary>
			/// This member is no longer used and is always set to zero. But this is module ID.
			/// </summary>
			internal UInt32 th32ModuleID;

			/// <summary>
			/// The number of execution threads started by the process
			/// </summary>
			internal UInt32 cntThreads;

			/// <summary>
			/// The identifier of the process that created this process (its parent process)
			/// </summary>
			internal UInt32 th32ParentProcessID;

			/// <summary>
			/// The base priority of any threads created by this process
			/// </summary>
			internal Int32 pcPriClassBase;

			/// <summary>
			/// This member is no longer used, and is always set to zero
			/// </summary>
			internal UInt32 dwFlags;

			/// <summary>
			/// he name of the executable file for the process
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string szExeFile;
		}

		/// <summary>
		/// Retrieves information about the first process encountered in a system snapshot
		/// </summary>
		/// <param name="hSnapshot">A handle to the snapshot from the CreateToolhelp32Snapshot</param>
		/// <param name="lppe">A pointer to a PROCESSENTRY32 structure</param>
		/// <returns>TRUE if the first entry of the process list in buffer or FALSE otherwise</returns>
		[DllImport("Kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern bool Process32First([In]IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		/// <summary>
		/// Retrieves information about the next process recorded in a system snapshot
		/// </summary>
		/// <param name="hSnapshot">A handle to the snapshot from the CreateToolhelp32Snapshot</param>
		/// <param name="lppe">A pointer to a PROCESSENTRY32 structure</param>
		/// <returns>TRUE if the first entry of the process list in buffer or FALSE otherwise</returns>
		[DllImport("Kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern bool Process32Next([In]IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)] 
		public struct THREADENTRY32
		{
			internal UInt32 dwSize;
			internal UInt32 cntUsage;           
			internal UInt32 th32ThreadID;       
			internal UInt32 th32OwnerProcessID;
			internal UInt32 tpBasePri;   
			internal Int32 tpDeltaPri;    
			internal UInt32 dwFlags;     
		}

		[DllImport("Kernel32")]
		public static extern bool Thread32First(IntPtr hSnapshot, ref THREADENTRY32 lpte);

		[DllImport("Kernel32")]
		public static extern bool Thread32Next(IntPtr hSnapshot, ref THREADENTRY32 lpte);

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct MODULEENTRY32
		{
			internal uint dwSize;
			internal uint th32ModuleID;
			internal uint th32ProcessID;
			internal uint GlblcntUsage;
			internal uint ProccntUsage;
			internal IntPtr modBaseAddr;
			internal uint modBaseSize;
			internal IntPtr hModule;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string szModule;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string szExePath;
		}

		[DllImport("Kernel32")]
		public static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

		[DllImport("Kernel32")]
		public static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
	}
}
