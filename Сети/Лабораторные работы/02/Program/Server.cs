using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    abstract class Server
    {
        protected long _node;
        protected ushort _socket;

        public Server(long node, ushort socket)
        {
            _node = node;
            _socket = socket;
        }

        public abstract void Start();
        public abstract void Send(byte[] buf);
        public abstract void Stop();
    }
}
