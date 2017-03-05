using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OS_lab_4
{
    class SystemInfo
    {
        public String ProcessorArchitecture {get; private set;}
        public uint PageSize { get; private set; }
        public IntPtr MinimumApplicationAddress { get; private set; }
        public IntPtr MaximumApplicationAddress { get; private set; }
        public IntPtr ActiveProcessorMask { get; private set; }
        public uint NumberOfProcessors { get; private set; }
        public uint AllocationGranularity { get; private set; }
        public String ProcessorLevel { get; private set; }
        public String ProcessorRevision { get; private set; }

        public SystemInfo()
        {
            var systemInfo = new win32API.SystemInfoStruct();
            win32API.GetSystemInfo(out systemInfo);

            switch (systemInfo.ProcessorArchitecture)
            {
                case win32API.ProcessorArchitecture.X86:
                    ProcessorArchitecture = "х86";
                    break;
                case win32API.ProcessorArchitecture.X64:
                    ProcessorArchitecture = "х64";
                    break;
                case win32API.ProcessorArchitecture.Arm:
                    ProcessorArchitecture = "ARM";
                    break;
                case win32API.ProcessorArchitecture.Itanium:
                    ProcessorArchitecture = "Intel Itanium";
                    break;
                default: 
                    ProcessorArchitecture = "Неизвестна"; 
                    break;
            }

            PageSize = systemInfo.PageSize;

            MinimumApplicationAddress = systemInfo.MinimumApplicationAddress;
            MaximumApplicationAddress = systemInfo.MaximumApplicationAddress;

            ActiveProcessorMask = systemInfo.ActiveProcessorMask;

            NumberOfProcessors = systemInfo.NumberOfProcessors;

            AllocationGranularity = systemInfo.AllocationGranularity;

            if (systemInfo.ProcessorArchitecture == win32API.ProcessorArchitecture.Itanium)
                ProcessorLevel = "1 (Intel Itanium)";
            else
                ProcessorLevel = systemInfo.ProcessorLevel + " (Intel)";

            ushort revision = systemInfo.ProcessorRevision;
            ProcessorRevision = "Модель " + ((revision >> 8) & 0xFF) + "  шаг " + (revision & 0xFF);
        }
    }
}
