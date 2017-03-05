using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Lab3
{
    abstract class Server
    {
        protected IPAddress _ip;
        protected ushort _socket;
        protected EndPoint _endpoint;

        public Server(string ip, ushort socket)
        {
            if (ip == null)
            {
                _ip = Utils.GetIP();
            }
            else
            {
                _ip = IPAddress.Parse(ip);
            }

            _socket = socket;
            _endpoint = new IPEndPoint(_ip, _socket);
        }

        public abstract void Start();
        public abstract void Send(byte[] buf);
        public abstract void Stop();
    }
}
