using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Udp;

namespace Server
{
    public class ServerUdp : Udp.Udp
    {
        public ServerUdp(System.Net.IPAddress address, int port) : base(address, port) { }

        // 1. socket
        // 2. bind
        // 3. listen
        // 4. accept

        public int Connect()
        {
            string stringData;
            byte[] data = new byte[0];

            IPEndPoint ipep = new IPEndPoint(this.Address, this.Port);

            // Creating UDP socket (datagram, using UDP)
            UdpClient socket = new UdpClient(ipep);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            Console.Out.WriteLine("Waiting for a client...");


            // Dictionary of connected clients
            Dictionary<string, ClientHandler> clients = new Dictionary<string, ClientHandler>();

            while (true)
            {
                data = socket.Receive(ref sender);
                stringData = Encoding.ASCII.GetString(data, 0, data.Length);

                Console.Out.WriteLine("< {0}: {1}", sender.ToString(), stringData);


                Message message = MessageSerializer.Deserialize(stringData);

                if (!clients.ContainsKey(sender.ToString()))
                {
                    clients[sender.ToString()] = new ClientHandler();
                }

                Message response = clients[sender.ToString()].ProceedRequest(message);
                string responseString = MessageSerializer.Serialize(response);
                data = Encoding.ASCII.GetBytes(responseString);

                Console.Out.WriteLine("> {0}: {1}", sender.ToString(), Encoding.ASCII.GetString(data));


                socket.Send(data, data.Length, sender);

                // Usuwanie klienta ze słownika na zakończenie sesji.
                if (response != null && response.Fields != null)
                {
                    string msg;
                    response.Fields.TryGetValue(ProtocolStrings.ResponseField, out msg);
                    if (msg == ProtocolStrings.ResponseFieldGoodbyeAction)
                    {
                        clients.Remove(sender.ToString());
                    }
                }
            }
        }

    }
}
