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
        public ClientUdp(System.Net.IPAddress address, int port) : base(address, port) { }

        // - socket
        // - connect

        public override int Run(bool verbose = false)
        {
            byte[] data = new byte[0];
            string input;
            string stringData;

            IPEndPoint ipep = new IPEndPoint(this.Address, this.Port);

            UdpClient socket = new UdpClient(ipep.Address.ToString(), ipep.Port);

            if (verbose)
            {
                Console.Out.WriteLine("Ready to send data...");
            }

            ServerHandler server = new ServerHandler(Guid.NewGuid().ToString());

            // Starts session
            Message messageBegin = server.CreateMessageBegin();
            string messageBeginString = MessageSerializer.Serialize(messageBegin);
            data = Encoding.ASCII.GetBytes(messageBeginString);

            if (verbose)
            {
                Console.Out.WriteLine("> {0}: {1}", ipep.ToString(), Encoding.ASCII.GetString(data));
            }

            socket.Send(data, data.Length);
            data = socket.Receive(ref ipep);
            stringData = Encoding.ASCII.GetString(data, 0, data.Length);

            if (verbose)
            {
                Console.Out.WriteLine("< {0}: {1}", ipep.ToString(), stringData);
            }

            while ((input = Console.In.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                OperationCommand cmd = ParseInput(input);
                Message message = server.CreateMessageRequest(cmd);
                string messageString = MessageSerializer.Serialize(message);

                data = Encoding.ASCII.GetBytes(messageString);

                if (verbose)
                {
                    Console.Out.WriteLine("> {0}: {1}", ipep.ToString(), Encoding.ASCII.GetString(data));
                }

                socket.Send(data, data.Length);
                data = socket.Receive(ref ipep);
                stringData = Encoding.ASCII.GetString(data, 0, data.Length);

                if (verbose)
                {
                    Console.Out.WriteLine("< {0}: {1}", ipep.ToString(), stringData);
                }

                Message response = MessageSerializer.Deserialize(stringData);
                if (response != null && response.Fields != null)
                {
                    string reponsedata = null;
                    response.Fields.TryGetValue("dane", out reponsedata);
                    if (reponsedata != null)
                    {
                        Console.Out.WriteLine(reponsedata);
                    }
                }
            }

            Message messageEnd = server.CreateMessageEnd();
            string messageEndString = MessageSerializer.Serialize(messageEnd);
            data = Encoding.ASCII.GetBytes(messageEndString);

            if (verbose)
            {
                Console.Out.WriteLine("> {0}: {1}", ipep.ToString(), Encoding.ASCII.GetString(data));
            }

            socket.Send(data, data.Length);
            data = socket.Receive(ref ipep);
            stringData = Encoding.ASCII.GetString(data, 0, data.Length);

            if (verbose)
            {
                Console.Out.WriteLine("< {0}: {1}", ipep.ToString(), stringData);
            }

            if (verbose)
            {
                Console.Out.WriteLine("[i] Stopping client");
            }
            socket.Close();

            return 0;
        }

        public OperationCommand ParseInput(string input)
        {
            OperationCommand cmd = new OperationCommand();

            string[] strs = input.Split(' ');
            if (strs.Length >= 0)
            {
                cmd.Operation = strs[0];
                List<int> arguments = new List<int>();
                int args = 1;

                for (int i = 1; i < strs.Length; i++)
                {
                    if (strs[i] == "end")
                    {
                        cmd.End = true;
                        break;
                    }
                    arguments.Add(int.Parse(strs[args]));
                    args++;

                    if (args > 3)
                    {
                        cmd.End = false;
                    }
                }

                cmd.Nums = arguments;
                cmd.NumsLength = args;
            }
            return cmd;
        }
    }
}
