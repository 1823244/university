using System.Net.Sockets;
using System.Net;

namespace Lab3
{
    class SPXClient : Client
    {
        private Socket _usingsocket;
        private EndPoint _endpoint;
        private EndPoint _localpoint;

        public SPXClient(long node, ushort socket) : base(node, socket)
        {
            _endpoint = new NetWare(0, node, socket);
            _localpoint = new NetWare(0, Utils.GetNumMAC(), socket);
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.Ipx, SocketType.Seqpacket, ProtocolType.Spx);
            _usingsocket.Bind(_localpoint);

            _usingsocket.Connect(_endpoint);
        }

        public override byte[] Receive(short length)
        {
            byte[] byteArray = new byte[length];

            _usingsocket.Receive(byteArray);

            return byteArray;
        }

        public override void Stop()
        {
            _usingsocket.Close();
        }
    }
}
