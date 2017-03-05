using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace OS_lab_4
{
    public class VirtualMap
    {
        public static uint countPage;

        public static List<String> GetVirtualMap(IntPtr handle)
        {
            countPage = 0;
            List<String> mapString = new List<string>();

            long MaxAddress = 0x7ffeffff;
            long address = 0;
            do
            {
                win32API.ProcessQueryInformation m;
                win32API.VirtualQueryEx(
                    handle,
                    (IntPtr)address,
                    out m,
                    (uint)Marshal.SizeOf(typeof(win32API.ProcessQueryInformation))
                    );

                if (m.State == win32API.QueryState.Free)
                {
                    mapString.Add(String.Format("0x{0} - 0x{1}     {2}",
                        Convert.ToString(m.BaseAdress.ToInt32(), 16).ToUpper(),
                        Convert.ToString((uint)m.BaseAdress - 1 + (uint)m.RegionSize, 16).ToUpper(),
                        "FREE"
                        )
                        );
                }
                else
                {
                    String protect = "";
                    switch (m.Protect)
                    {
                        case win32API.ProtectionConstant.Execute:
                            protect = "Execute";
                            break;
                        case win32API.ProtectionConstant.ExecuteAndRead:
                            protect = "ExecuteAndRead";
                            break;
                        case win32API.ProtectionConstant.ExecuteAndReadAndWrite:
                            protect = "ExecuteAndReadAndWrite";
                            break;
                        case win32API.ProtectionConstant.ExecuteWithWriteCopy:
                            protect = "ExecuteWithWriteCopy";
                            break;
                        case win32API.ProtectionConstant.NoAccess:
                            protect = "NoAccess";
                            break;
                        case win32API.ProtectionConstant.ReadOnly:
                            protect = "ReadOnly";
                            break;
                        case win32API.ProtectionConstant.ReadAndWrite:
                            protect = "ReadAndWrite";
                            break;
                        case win32API.ProtectionConstant.WriteCopy:
                            protect = "WriteCopy";
                            break;
                        case win32API.ProtectionConstant.Guard:
                            protect = "Guard";
                            break;
                        case win32API.ProtectionConstant.NoCache:
                            protect = "NoCache";
                            break;
                        case win32API.ProtectionConstant.WriteCombine:
                            protect = "WriteCombine";
                            break;
                    }
                    countPage += ((uint)m.RegionSize) / 1024 / 4;
                    mapString.Add(String.Format("0x{0} - 0x{1}     {2}     {3}     {4}     {5}",
                        Convert.ToString(m.BaseAdress.ToInt32(), 16).ToUpper(),
                        Convert.ToString((uint)m.BaseAdress - 1 + (uint)m.RegionSize, 16).ToUpper(),
                        (((uint)m.RegionSize) / 1024 / 4) + "p",
                        (m.State == win32API.QueryState.Commit ? "COMMIT" : "RESERVE"),
                        (m.Type == win32API.MemoryType.Private ? "PRIVATE" : (m.Type == win32API.MemoryType.Mapped ? "MAPPED" : "IMAGE")),
                        protect
                        )
                        );
                }

                if (address == (long)m.BaseAdress + (long)m.RegionSize)
                    break;
                address = (long)m.BaseAdress + (long)m.RegionSize;
            } while (address <= MaxAddress);

            return mapString;
        }
    }
}
