using System.Net.Sockets;

namespace Lab4
{
    class UdpClient : Client
    {
        private Socket _usingsocket;

        public UdpClient(string ip, ushort socket) : base(ip, socket)
        {
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
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
