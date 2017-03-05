using System;
using System.Net;
using System.Net.Sockets;

namespace Lab3
{
    public class NetWare : EndPoint
    {
        private const int SocketSize = 14;

        public uint Network;
        public long Node;
        public ushort Socket;

        public NetWare(uint network, long node, ushort socket)
        {
            Network = network;
            Node = node;
            Socket = socket;
        }

        public override SocketAddress Serialize()
        {
            var address = new SocketAddress(AddressFamily.Ipx, SocketSize);

            byte[] network = BitConverter.GetBytes(Network);
            
            // GetBytes возвращает биты в обратном порядке!
            Array.Reverse(network);

            // первые два байта занимает AddressFamily,
            // номер сети вставляем в байты 2-5
            for (int i = 0; i < 4; i++)
            {
                address[i + 2] = network[i];
            }

            byte[] node = BitConverter.GetBytes(Node);
            Array.Reverse(node);

            // адрес рабочей станции (мак) с 6 по 11 байты
            // node -- это long, поэтому опускаем 0 и 1 байты
            for (int i = 0; i < 6; i++)
            {
                address[i + 6] = node[i + 2];
            }

            byte[] socket = BitConverter.GetBytes(Socket);
            Array.Reverse(socket);

            // сокет -- это 12 и 13 байты
            for (int i = 0; i < 2; i++)
            {
                address[i + 12] = socket[i];
            }

            return address;
        }

        public override AddressFamily AddressFamily
        {
            get
            {
                return System.Net.Sockets.AddressFamily.Ipx;
            }
        }

        public override EndPoint Create(SocketAddress socketAddress)
        {
            byte[] network = new byte[4];

            for (int i = 0; i < network.Length; i++)
            {
                network[i] = socketAddress[i + 2];
            }

            byte[] node = new byte[8];

            for (int i = 0; i < node.Length; i++)
            {
                node[i] = socketAddress[i + 6];
            }

            byte[] socket = new byte[2];

            for (int i = 0; i < socket.Length; i++)
            {
                socket[i] = socketAddress[i + 12];
            }

            return new NetWare(BitConverter.ToUInt32(network, 0), BitConverter.ToInt64(node, 0), BitConverter.ToUInt16(socket, 0));  
        }
    }
}
