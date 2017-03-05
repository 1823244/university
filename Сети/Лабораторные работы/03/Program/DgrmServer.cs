using System.Net.Sockets;

namespace Lab3
{
    class DgrmServer : Server
    {
        private Socket _usingsocket;

        public DgrmServer(string ip, ushort socket) : base(ip, socket)
        {
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
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
