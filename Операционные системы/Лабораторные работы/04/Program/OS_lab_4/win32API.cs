using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace OS_lab_4
{
    static class win32API
    {
        [DllImport("kernel32", SetLastError = true)]
        public static extern void GetSystemInfo(out SystemInfoStruct lpSystemInfo);

        public enum ProcessorArchitecture
        {
            X86 = 0,
            X64 = 9,
            Arm = -1,
            Itanium = 6,
            Unknown = 0xFFFF,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemInfoStruct
        {
            public ProcessorArchitecture ProcessorArchitecture;
            public uint PageSize;
			public IntPtr MinimumApplicationAddress;
            public IntPtr MaximumApplicationAddress;
            public IntPtr ActiveProcessorMask;
            public uint NumberOfProcessors;
            public uint ProcessorType;
            public uint AllocationGranularity;
            public ushort ProcessorLevel;
            public ushort ProcessorRevision;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatus(ref MEMORYSTATUS lpBuffer);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MEMORYSTATUS
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public UIntPtr dwTotalPhys;
            public UIntPtr dwAvailPhys;
            public UIntPtr dwTotalPageFile;
            public UIntPtr dwAvailPageFile;
            public UIntPtr dwTotalVirtual;
            public UIntPtr dwAvailVirtual;
        }

        [DllImport("kernel32.dll")]
        public static extern int VirtualQueryEx(
             IntPtr handle,
             IntPtr adress,
             out ProcessQueryInformation processQuery,
             uint length
        );

        public enum ProtectionConstant
        {
            Execute = 0x10,
            ExecuteAndRead = 0x20,
            ExecuteAndReadAndWrite = 0x40,
            ExecuteWithWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadAndWrite = 0x04,
            WriteCopy = 0x08,
            Guard = 0x100,
            NoCache = 0x200,
            WriteCombine = 0x400,
        }

        public enum QueryState
        {
            Commit = 0x1000,
            Free = 0x10000,
            Reserved = 0x2000,
        }

        public enum MemoryType
        {
            Image = 0x1000000,
            Mapped = 0x40000,
            Private = 0x20000,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ProcessQueryInformation
        {
            public IntPtr BaseAdress;
            public IntPtr AllocationBase;
            public ProtectionConstant AllocationProtect;
            public uint RegionSize;
            public QueryState State;
            public ProtectionConstant Protect;
            public MemoryType Type;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UIntPtr VirtualAlloc(UIntPtr lpAddress, UIntPtr dwSize,
           AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualFree(UIntPtr lpAddress, UIntPtr dwSize,
           FreeType dwFreeType);

        [Flags()]
        public enum AllocationType : uint
        {
            COMMIT = 0x1000,
            RESERVE = 0x2000,
            RESET = 0x80000,
            LARGE_PAGES = 0x20000000,
            PHYSICAL = 0x400000,
            TOP_DOWN = 0x100000,
            WRITE_WATCH = 0x200000
        }

        [Flags()]
        public enum FreeType : uint
        {
            DECOMMIT = 0x4000,
            RELEASE = 0x8000
        }

        [Flags()]
        public enum MemoryProtection : uint
        {
            EXECUTE = 0x10,
            EXECUTE_READ = 0x20,
            EXECUTE_READWRITE = 0x40,
            EXECUTE_WRITECOPY = 0x80,
            NOACCESS = 0x01,
            READONLY = 0x02,
            READWRITE = 0x04,
            WRITECOPY = 0x08,
            GUARD_Modifierflag = 0x100,
            NOCACHE_Modifierflag = 0x200,
            WRITECOMBINE_Modifierflag = 0x400
        }
    }
}
