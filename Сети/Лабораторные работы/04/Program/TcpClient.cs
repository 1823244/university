using System.Net.Sockets;
using System.Net;

namespace Lab4
{
    class TcpClient : Client
    {
        private Socket _usingsocket;
        private EndPoint _localpoint;

        public TcpClient(string ip, ushort socket) : base(ip, socket)
        {
            _localpoint = new IPEndPoint(Utils.GetIP(), socket);
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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
