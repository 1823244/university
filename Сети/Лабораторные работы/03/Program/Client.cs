using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Lab3
{
    abstract class Client
    {
        protected IPAddress _ip;
        protected ushort _socket;
        protected EndPoint _endpoint;

        public Client(string ip, ushort socket)
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
        public abstract byte[] Receive(short length);
        public abstract void Stop();
    }
}
