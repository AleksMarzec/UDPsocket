using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Udp;

namespace Client
{
    public class ClientUdp : Udp.Udp
    {
        private byte[] data { get; set; }
        IPEndPoint ipep;
        UdpClient socket { get; set; }
        ServerHandler server { get; set; }

        public ClientUdp(System.Net.IPAddress address, int port) : base(address, port)
        {
            data = new byte[0];
            ipep = new IPEndPoint(this.Address, this.Port);
            socket = new UdpClient(ipep.Address.ToString(), ipep.Port);
            server = new ServerHandler(Guid.NewGuid().ToString());
        }

        // - socket
        // - connect

        public Tuple<string, string> Connect()
        {
            // Starts session
            Message messageBegin = server.CreateMessageBegin();
            string messageBeginString = MessageSerializer.Serialize(messageBegin);
            data = Encoding.ASCII.GetBytes(messageBeginString);

            socket.Send(data, data.Length);
            string sended = ($"> {ipep.ToString()}: {Encoding.ASCII.GetString(data)}");

            data = socket.Receive(ref ipep);
            string stringData = Encoding.ASCII.GetString(data, 0, data.Length);
            string received = ($"< {ipep.ToString()}: {stringData}");

            return new Tuple<string, string>(sended, received);
        }

        public Tuple<string, string, string> Run(OperationCommand Parameters)
        {
            Message message = server.CreateMessageRequest(Parameters);
            string messageString = MessageSerializer.Serialize(message);

            data = Encoding.ASCII.GetBytes(messageString);
            socket.Send(data, data.Length);
            string sended = ($"> {ipep.ToString()}: {Encoding.ASCII.GetString(data)}");

            data = socket.Receive(ref ipep);
            string stringData = Encoding.ASCII.GetString(data, 0, data.Length);
            string received = ($"< {ipep.ToString()}: {stringData}");

            Message response = MessageSerializer.Deserialize(stringData);
            string responsedata = "blad";
            if (response != null && response.Fields != null)
            {
                response.Fields.TryGetValue("dane", out responsedata);
            }

            return new Tuple<string, string, string>(sended, received, responsedata);
        }

        public Tuple<string, string> EndConnection()
        {
            Message messageEnd = server.CreateMessageEnd();
            string messageEndString = MessageSerializer.Serialize(messageEnd);
            data = Encoding.ASCII.GetBytes(messageEndString);
            string sended = ($"> {ipep.ToString()}: {Encoding.ASCII.GetString(data)}");


            socket.Send(data, data.Length);
            data = socket.Receive(ref ipep);
            string stringData = Encoding.ASCII.GetString(data, 0, data.Length);

         
            string received = ($"< {ipep.ToString()}: {stringData}");

            socket.Close();
            return new Tuple<string, string>(sended, received);
        }

    }
}
