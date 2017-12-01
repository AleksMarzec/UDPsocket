using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udp
{
    public abstract class Udp
    {
        // IP Address
        public System.Net.IPAddress Address { get; set; }

        // Port number
        public int Port { get; set; }

        // Size of packet / buffor for sending and receiving data
        public int Size { get; protected set; } = 1024;

        public Udp(System.Net.IPAddress address, int port)
        {
            this.Address = address;
            this.Port = port;
        }
    }
}
