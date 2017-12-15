using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udp
{
    // Klasa abstrakcyjna.
    public abstract class Udp
    {
        public System.Net.IPAddress Address { get; set; } // Adres IP
        public int Port { get; set; } // Numer portu
        public int Size { get; protected set; } = 1024; // Rozmiar bufora służącego do odbioru i odczytu danych / rozmiar komunikatu

        public Udp(System.Net.IPAddress address, int port)
        {
            this.Address = address;
            this.Port = port;
        }
    }
}
