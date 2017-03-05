using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Net;

namespace Lab3
{
    class Utils
    {
        public static string GetMAC()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }

        public static string GetIPString()
        {
            return GetIP().ToString();
        }

        public static IPAddress GetIP()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        }

        public static void Starter(bool mode, bool protocol, string ip, ushort socket, MainForm form)
        {
            short packetSize = 512;
            short dataSize = Convert.ToInt16(packetSize - 8);

            if (mode == true)
            {
                Client client;

                if (protocol == true)
                {
                    client = new DgrmClient(null, socket);
                }
                else
                {
                    client = new StrmClient(ip, socket);
                }

                var fs = new FileStream(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                        @"\img-" + socket + ".jpg", FileMode.Create, FileAccess.Write);

                client.Start();

                byte[] byteArray = new byte[packetSize];
                var maxDataBlock = 21; // mb
                maxDataBlock = (maxDataBlock * 1024 * 1024 / dataSize) * dataSize;
                byte[] saver = new byte[maxDataBlock];

                long received = 0;
                long length = 0;
                var dataBlock = 0;

                byteArray = client.Receive(packetSize);

                if (byteArray.Any(b => b != 0))
                {
                    byte[] tmp = new byte[8];
                    Array.Copy(byteArray, dataSize, tmp, 0, 8);
                    length = BitConverter.ToInt64(tmp, 0);

                    while (byteArray.Any(b => b != 0))
                    {
                        Array.Copy(byteArray, 0, saver, dataBlock, dataSize);
                        
                        dataBlock += dataSize;

                        if (dataBlock == maxDataBlock)
                        {
                            fs.Write(saver, 0, dataBlock);
                            dataBlock = 0;

                            Array.Clear(saver, 0, saver.Length);
                        }
                        
                        byteArray = client.Receive(packetSize);
                        received += dataSize;

                        form.UpdateProgess(Convert.ToInt32((received * 100) / length));
                    }

                    fs.Write(saver, 0, dataBlock);
                }
                
                client.Stop();

                double lost = length - received;
                lost = lost < 0 ? 0 : lost;
                received = received > length ? length : received;

                form.UpdateStatus("Получено: " + String.Format("{0:N0}", received) + " байт; " +
                                                     "потеряно: " + String.Format("{0:N0}", Convert.ToInt32(lost)) + " байт " +
                                                     "(" + String.Format("{0:F2}", lost / length * 100) + "%)");

                fs.Close();
                fs.Dispose();
            }
            else
            {
                Server server;

                if (protocol == true)
                {
                    server = new DgrmServer(ip, socket);
                }
                else
                {
                    server = new StrmServer(null, socket);
                }

                var fs = new FileStream(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                        @"\img.jpg", FileMode.Open, FileAccess.Read);

                byte[] byteArray = new byte[packetSize];

                long length = fs.Length;
                var fSize = BitConverter.GetBytes(length);
                long total = 0;
                
                Stopwatch timer = Stopwatch.StartNew();

                server.Start();

                while (fs.Read(byteArray, 0, dataSize) > 0)
                {
                    fSize.CopyTo(byteArray, dataSize);

                    server.Send(byteArray);

                    total += dataSize;

                    form.UpdateProgess(Convert.ToInt32((total * 100) / length));

                    Array.Clear(byteArray, 0, byteArray.Length);
                }

                for (int i = 0; i < 100; i++)
                {
                    server.Send(byteArray);
                    Thread.Sleep(1);
                }

                server.Stop();

                timer.Stop();

                form.UpdateStatus("Передано " + String.Format("{0:N0}", length) + " байт " +
                                                     "за " + String.Format("{0:N2}", Convert.ToDouble(timer.ElapsedMilliseconds) / 1000) + " с");

                fs.Close();
                fs.Dispose();
            }
        }
    }
}
