using System.Net.Sockets;

namespace Lab4
{
    class UdpServer : Server
    {
        private Socket _usingsocket;

        public UdpServer(string ip, ushort socket) : base(ip, socket)
        {
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
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
