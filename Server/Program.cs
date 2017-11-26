using System;
using System.Collections.Generic;
#if NDESK_OPTIONS
using NDesk.Options;
#else
using Mono.Options;
#endif

namespace Server
{
    public class MainClass : Udp.Program
    {
        public static void Main(string[] args)
        {
            string portString = "8888";
            bool verbose = false;
            bool version = false;
            bool help = false;
            OptionSet options = new OptionSet()
            {
                {"p|port=", string.Format("Port number(default={0}.", portString), option => portString = option },
                { "v|verbose", "Enable verbose mode.", option => verbose = option != null},
                { "V|version", "Show program version.", option => version = option != null},
                {"h|help", "Prints out the options.", option => help = option != null }
            };

            List<string> extra = new List<string>();

            try
            {
                // Parse the command line.
                extra = options.Parse(args);
            }
            catch (OptionException ex)
            {
                Console.Error.WriteLine(ex.Message);
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
                Environment.Exit(0);
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
            System.Net.IPAddress address = System.Net.IPAddress.Any;
            int port = default(int);

            try
            {
                port = Int32.Parse(portString);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Invalid port number.");
                if (ex.GetType().Name.Length > 0)
                {
                    Console.Error.WriteLine("Exception type: {0}", ex.GetType().Name);
                }
                Console.WriteLine($"Message: {ex.Message}.");
                Console.Error.WriteLine();
                status = 1;
            }

            if (status != 0)
            {
                PrintHelp(options, Console.Out);
                Environment.Exit(status);
            }

            ServerUdp server = new ServerUdp(address, port);
            status = server.Run(verbose);
            Environment.Exit(status);
        }
    }
}
