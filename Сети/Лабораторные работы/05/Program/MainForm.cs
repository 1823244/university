using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace Lab5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ShowARPTable();
        }

        #region events

        private void addARP_Click(object sender, EventArgs e)
        {
            var arpStream = ExecuteCommandLine("arp", "-s " + addIP.Text + " " + addMAC.Text).ReadToEnd();
            if (arpStream == "")
            {
                ShowARPTable();
            }
            else
            {
                arpTable.Clear();
                arpTable.Text += arpStream;
            }
        }

        private void delARP_Click(object sender, EventArgs e)
        {
            var arpStream = ExecuteCommandLine("arp", "-d " + delIP.Text).ReadToEnd();

            if (arpStream == "")
            {
                ShowARPTable();
            }
            else
            {
                arpTable.Clear();
                arpTable.Text += arpStream;
            }
        }

        private void ARPTableRefresh_Click(object sender, EventArgs e)
        {
            ShowARPTable();
        }

        private void findMAC_Click(object sender, EventArgs e)
        {
            try
            {
                var dst = IPAddress.Parse(findIP.Text);
                var macAddr = new byte[6];
                var macAddrLen = (uint)macAddr.Length;
                var intIP = BitConverter.ToInt32(dst.GetAddressBytes(), 0);

                if (SendARP(intIP, 0, macAddr, ref macAddrLen) != 0)
                    throw new InvalidOperationException("SendARP failed.");

                var str = new string[macAddrLen];

                for (int i = 0; i < macAddrLen; i++)
                    str[i] = macAddr[i].ToString("x2");

                findMACLabel.Text = string.Join(":", str);
                findMACLabel.Visible = true;
            }
            catch (Exception ex)
            {
                findMACLabel.Text = "неизвестен.";
                findMACLabel.Visible = true;
            }
        }

        #endregion

        #region imports etc

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_IPNETROW
        {
            public int Index;
            public int PhysAddrLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] PhysAddr;
            public int Addr;
            public int Type;
        }

        public enum MIB_IPNET_TYPE
        {
            OTHER = 1,
            INVALID = 2,
            DYNAMIC = 3,
            STATIC = 4
        }

        [DllImport("iphlpapi.dll")]
        public extern static int GetIpNetTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder);

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int CreateIpNetEntry(IntPtr pArpEntry);

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public extern static int DeleteIpNetEntry(IntPtr pArpEntry);

        #endregion

        public static StreamReader ExecuteCommandLine(String file, String arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = file;
            startInfo.Arguments = arguments;

            Process process = Process.Start(startInfo);

            return process.StandardOutput;
        }

        private string MACFormat(byte[] m)
        {
            return string.Format("{0:x2}-{1:x2}-{2:x2}-{3:x2}-{4:x2}-{5:x2}", m[0], m[1], m[2], m[3], m[4], m[5]);
        }

        private string IPFormat(int ip)
        {
            var b = BitConverter.GetBytes(ip);
            return string.Format("{0}.{1}.{2}.{3}", b[0], b[1], b[2], b[3]);
        }

        public void ShowARPTable()
        {
            var type = new string[] { "", "другой", "недопустимый", "динамический", "статический" };
            var arp = new List<MIB_IPNETROW>();
            var size = 0;
            GetIpNetTable(IntPtr.Zero, ref size, true);
            var p = Marshal.AllocHGlobal(size);

            if (GetIpNetTable(p, ref size, true) == 0)
            {
                var num = Marshal.ReadInt32(p);
                var ptr = IntPtr.Add(p, 4);

                for (int i = 0; i < num; i++)
                {
                    arp.Add((MIB_IPNETROW)Marshal.PtrToStructure(ptr, typeof(MIB_IPNETROW)));
                    ptr = IntPtr.Add(ptr, Marshal.SizeOf(typeof(MIB_IPNETROW)));
                }

                Marshal.FreeHGlobal(p);
            }

            arpTable.Clear();

            foreach (var i in arp.GroupBy(x => x.Index))
            {
                arpTable.Text += string.Format("Интерфейс: --- 0x{0:x}", i.Key) + '\n';
                arpTable.Text += "  адрес в Интернете\t Физический адрес\t тип" + '\n';
                foreach (var n in arp.Where(x => x.Index == i.Key))
                {
                    arpTable.Text += string.Format("  {0,-15}\t\t {1}\t\t {2}", 
                                                    IPFormat(n.Addr), 
                                                    MACFormat(n.PhysAddr), 
                                                    type[n.Type]) + '\n';
                }

                arpTable.Text += '\n';
            }
        }
    }
}
