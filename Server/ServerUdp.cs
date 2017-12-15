using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
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

            // Tworzenie gniazda datagramowego wykorzystującego protokół UDP.
            UdpClient socket = new UdpClient();

            // Tylko jeden proces może zbindować się do danego portu (ExclusiveAddressUse).
            // https://msdn.microsoft.com/en-us/library/system.net.sockets.udpclient.exclusiveaddressuse(v=vs.110).aspx
             socket.ExclusiveAddressUse = true;

            try
            {
                socket.Client.Bind(ipep);
            }
            catch
            {
                Console.Error.WriteLine("[!] Error: Unable to bind socket.");
                return 1;
            }

            // Przyjmujemy dane z dowolnego źródła.
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            Console.Out.WriteLine("Waiting for a client...");


            // Słownik do obsługi klientów.
            Dictionary<string, ClientHandler> clients = new Dictionary<string, ClientHandler>();

            while (true)
            {
                // Bufor nie musi być za każdym razem czyszczony - ten problem rozwiązuje klasa UdpClient.
                // data = new byte[this.Size].
                data = socket.Receive(ref sender);
                stringData = Encoding.ASCII.GetString(data, 0, data.Length);

                // Aby móc rózróżnić poszczególnych nadawców komunikatów, każdy klient jest rozróżniany w słowniku
                // poprzez swój endpoint.
                Console.Out.WriteLine("< {0}: {1}", sender.ToString(), stringData);

                // Właściwa część obsługi protokołu.
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

                // Usuwanie klienta ze słownika w przypadku zakończenia sesji.
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
