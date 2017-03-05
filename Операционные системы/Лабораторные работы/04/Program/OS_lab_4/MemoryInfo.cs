using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OS_lab_4
{
    class MemoryInfo
    {
        public uint MemoryLoad { get; private set; }
        public UIntPtr TotalPhys { get; private set; }
        public UIntPtr AvailPhys { get; private set; }
        public UIntPtr TotalPageFile { get; private set; }
        public UIntPtr AvailPageFile { get; private set; }
        public UIntPtr TotalVirtual { get; private set; }
        public UIntPtr AvailVirtual { get; private set; }


        public uint pageSize { get; set; }
        public uint pageLimit { get; set; }
        private List<UIntPtr> baseAddresses = new List<UIntPtr>();


        public MemoryInfo()
        {
        }

        public void update()
        {
            var tmp = new win32API.MEMORYSTATUS();
            win32API.GlobalMemoryStatus(ref tmp);

            MemoryLoad = tmp.dwMemoryLoad;

            TotalPhys = tmp.dwTotalPhys;
            AvailPhys = tmp.dwAvailPhys;

            TotalPageFile = tmp.dwTotalPageFile;
            AvailPageFile = tmp.dwAvailPageFile;

            TotalVirtual = tmp.dwTotalVirtual;
            AvailVirtual = tmp.dwAvailVirtual;
        }

        public void VirtualAlloc()
        {
            if (pageSize > 0 && pageLimit > 0)
            {
                UIntPtr address = win32API.VirtualAlloc(
                    UIntPtr.Zero,
                    new UIntPtr(pageSize * pageLimit),
                    win32API.AllocationType.RESERVE | win32API.AllocationType.COMMIT,
                    win32API.MemoryProtection.NOACCESS
                    );

                if (address != UIntPtr.Zero)
                {
                    baseAddresses.Add(address);
                }
            }
        }

        public void VirtualFree()
        {
            if (baseAddresses.Count > 0)
            {
                UIntPtr adress = baseAddresses[0];
                baseAddresses.RemoveAt(0);

                win32API.VirtualFree(
                    adress,
                    UIntPtr.Zero,
                    win32API.FreeType.RELEASE
                    );
            }
        }
    }
}
