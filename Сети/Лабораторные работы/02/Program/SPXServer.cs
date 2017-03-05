using System.Net.Sockets;
using System.Net;

namespace Lab3
{
    class SPXServer : Server
    {
        private Socket _usingsocket;
        private Socket _acceptsocket;
        private EndPoint _endpoint;

        public SPXServer(long node, ushort socket) : base(node, socket)
        {
            _endpoint = new NetWare(0, node, socket);
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.Ipx, SocketType.Seqpacket, ProtocolType.Spx);

            _usingsocket.Bind(_endpoint);
            _usingsocket.Listen(10);
            _acceptsocket = _usingsocket.Accept();
        }

        public override void Send(byte[] buf)
        {
            try
            {
                _acceptsocket.Send(buf);
            }
            catch
            {
                return;
            }
        }

        public override void Stop()
        {
            _usingsocket.Close();
            _acceptsocket.Close();
        }
    }
}
