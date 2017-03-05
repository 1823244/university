using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Net;

namespace Lab6
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        const int MAX_ADAPTER_NAME = 128;
        const int ERROR_INSUFFICIENT_BUFFER = 122;
        const int ERROR_SUCCESS = 0;

        [DllImport("Iphlpapi.dll", CharSet = CharSet.Auto)]
        static extern int IpReleaseAddress(ref IP_ADAPTER_INDEX_MAP AdapterInfo);

        [DllImport("Iphlpapi.dll", CharSet = CharSet.Auto)]
        static extern int IpRenewAddress(ref IP_ADAPTER_INDEX_MAP AdapterInfo);

        [DllImport("Iphlpapi.dll", CharSet = CharSet.Auto)]
        static extern int GetInterfaceInfo(IntPtr PIfTableBuffer, ref int size);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct IP_ADAPTER_INDEX_MAP
        {
            public int Index;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ADAPTER_NAME)]
            public String Name;
        }

        private void aboutDHCP_Click(object sender, EventArgs e)
        {
            resultInfo.Clear();

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                IPAddressCollection addresses = adapterProperties.DhcpServerAddresses;

                if (addresses.Count > 0)
                {
                    resultInfo.Text += adapter.Description + '\n';

                    foreach (IPAddress address in addresses)
                    {
                        resultInfo.Text += "Адрес: " + address.ToString() + '\n';
                    }

                    resultInfo.Text += '\n';
                }
            }
        }

        private void resetIP_Click(object sender, EventArgs e)
        {
            resultInfo.Clear();

            var list = new List<IP_ADAPTER_INDEX_MAP>();
            int size = 0;
            if (GetInterfaceInfo(IntPtr.Zero, ref size) == ERROR_INSUFFICIENT_BUFFER)
            {
                var p = Marshal.AllocHGlobal(size);

                if (GetInterfaceInfo(p, ref size) == ERROR_SUCCESS)
                {
                    var num = Marshal.ReadInt32(p);
                    var next = IntPtr.Add(p, 4);

                    for (int i = 0; i < num; i++)
                    {
                        var adapter = (IP_ADAPTER_INDEX_MAP)Marshal.PtrToStructure(next, typeof(IP_ADAPTER_INDEX_MAP));
                        list.Add(adapter);
                        next = IntPtr.Add(next, Marshal.SizeOf(typeof(IP_ADAPTER_INDEX_MAP)));
                    }
                }

                Marshal.Release(p);
            }

            for (int i = 0; i < list.Count; i++)
            {
                var el = list[i];
                IpReleaseAddress(ref el);
                list[i] = el;
            }

            resultInfo.Text += "IP сброшен.\n";

            for (int i = 0; i < list.Count; i++)
            {
                var el = list[i];
                IpRenewAddress(ref el);
                list[i] = el;
            }

            resultInfo.Text += "Новый IP запрошен.\n";
        }

        private void findIP_Click(object sender, EventArgs e)
        {
            resultInfo.Clear();

            try
            {
                var host = Dns.GetHostEntry(domain.Text);
                resultInfo.Text += "IP-адреса домена:\n";

                foreach (var ip in host.AddressList)
                {
                    resultInfo.Text += "   " + ip.ToString() + '\n';
                }
            }
            catch
            {
                resultInfo.Text += "Неизвестный домен.\n";
            }
        }

        private void findDomain_Click(object sender, EventArgs e)
        {
            resultInfo.Clear();

            try
            {
                var ipObject = IPAddress.Parse(ip.Text);
                var host = Dns.GetHostEntry(ipObject);

                resultInfo.Text += "Адресу " + ip.Text + " соответствует домен "
                                   + host.HostName + '.';
            }
            catch
            {
                resultInfo.Text += "IP указан неверно.";
            }
        }
    }
}