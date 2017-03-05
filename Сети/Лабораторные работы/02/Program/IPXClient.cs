using System.Net.Sockets;
using System.Net;

namespace Lab3
{
    class IPXClient : Client
    {
        private Socket _usingsocket;
        private EndPoint _endpoint;

        public IPXClient(long node, ushort socket) : base(node, socket)
        {
            _endpoint = new NetWare(0, 0xffffffffffff, socket);
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.Ipx, SocketType.Dgram, ProtocolType.Ipx);
            _usingsocket.Bind(_endpoint);
        }

        public override byte[] Receive(short length)
        {
            byte[] byteArray = new byte[length];

            _usingsocket.ReceiveFrom(byteArray, ref _endpoint);

            return byteArray;
        }

        public override void Stop()
        {
            _usingsocket.Close();
        }
    }
}
