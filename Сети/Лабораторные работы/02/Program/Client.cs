using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    abstract class Client
    {
        protected long _node;
        protected ushort _socket;

        public Client(long node, ushort socket)
        {
            _node = node;
            _socket = socket;
        }

        public abstract void Start();
        public abstract byte[] Receive(short length);
        public abstract void Stop();
    }
}
