using System.Net.Sockets;

namespace Lab3
{
    class StrmServer : Server
    {
        private Socket _usingsocket;
        private Socket _acceptsocket;

        public StrmServer(string ip, ushort socket) : base(ip, socket)
        {
        }

        public override void Start()
        {
            _usingsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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
