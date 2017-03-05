using System.Net.Sockets;
using System.Net;

namespace Lab3
{
    class IPXServer : Server
    {
        private Socket _usingsocket;
        private EndPoint _endpoint;

        public IPXServer(long node, ushort socket) : base(node, socket)
        {
            _endpoint = new NetWare(0, node, socket);
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.Ipx, SocketType.Dgram, ProtocolType.Ipx);
        }

        public override void Send(byte[] buf)
        {
            _usingsocket.SendTo(buf, _endpoint);
        }

        public override void Stop()
        {
            _usingsocket.Close();
        }
    }
}
