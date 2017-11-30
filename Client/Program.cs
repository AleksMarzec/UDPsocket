using System;
using System.Collections.Generic;
#if NDESK_OPTIONS
using NDesk.Options;
#else
using Mono.Options;
#endif

namespace Client
{
    public class MainClass : Udp.Program
    {
        public static void Main(string[] args)
        {
            string addressString = "127.0.0.1";
            string portString = "8888";
            bool verbose = false;
            bool version = false;
            bool help = false;
            OptionSet options = new OptionSet()
            {
                { "a|address=", string.Format("Address (defaulut={0}).", addressString), option => addressString = option },
                { "p|port=", string.Format("Port number (defaulut={0}).", portString), option => portString = option },
                { "v|verbose", "Enable verbose mode.", option => verbose = option != null },
                { "V|version", "Show program version.", option => version = option != null },
                { "h|help", "Prints out the options.", option => help = option != null }
            };

            List<string> extra = new List<string>();
            try
            {
                // Parse the command line
                extra = options.Parse(args);
            }
            catch (OptionException e)
            {
                // Error
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine();
                PrintHelp(options, Console.Out);
                Environment.Exit(1);
            }

            if (extra.Count > 0)
            {
                Console.Error.WriteLine("Unexpected arguments:");
                foreach (string arg in extra)
                {
                    Console.Error.WriteLine("- {0}", arg);
                }
                Console.Error.WriteLine();
                PrintHelp(options, Console.Out);
                Environment.Exit(1);
            }

            if (help)
            {
                PrintHelp(options, Console.Out);
                Environment.Exit(0);
            }

            if (version)
            {
                PrintProgramInfo(Console.Out);
                Environment.Exit(0);
            }

            int status = 0;
            System.Net.IPAddress address = default(System.Net.IPAddress);
            int port = default(int);

            try
            {
                address = System.Net.IPAddress.Parse(addressString);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Invalid address!");
                if (e.GetType().Name.Length > 0)
                {
                    Console.Error.WriteLine("Exception type: {0}", e.GetType().Name);
                }
                Console.Error.WriteLine("Message: " + e.Message);
                Console.Error.WriteLine();
                status = 1;
            }

            try
            {
                port = Int32.Parse(portString);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Invalid port number!");
                if (e.GetType().Name.Length > 0)
                {
                    Console.Error.WriteLine("Exception type: {0}", e.GetType().Name);
                }
                Console.Error.WriteLine("Message: " + e.Message);
                Console.Error.WriteLine();
                status = 1;
            }

            if (status != 0)
            {
                PrintHelp(options, Console.Out);
                Environment.Exit(status);
            }

            ClientUdp client = new ClientUdp(address, port);
            status = client.Run(verbose);
            Environment.Exit(status);
        }
    }
}
