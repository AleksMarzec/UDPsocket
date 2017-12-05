using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udp
{
    public abstract class Udp
    {
        // Adres IP
        public System.Net.IPAddress Address { get; set; }

        // Numer portu
        public int Port { get; set; }

        // Rozmiar bufora służącego do odbioru i odczytu danych / rozmiar komunikatu
        public int Size { get; protected set; } = 1024;

        public Udp(System.Net.IPAddress address, int port)
        {
            this.Address = address;
            this.Port = port;
        }
    }
}
