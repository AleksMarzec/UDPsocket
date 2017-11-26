using System;
using System.Diagnostics;
#if NDESK_OPTIONS
using NDesk.Options;
#else
using Mono.Options;
#endif

namespace Udp
{
    public class Program
    {
        public static string GetProgramName()
        {
            string text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            return text;
        }

        public static string GetProgramFilename()
        {
            string text = Environment.GetCommandLineArgs()[0];
            return text;
        }

        public static string GetProgramVersion(bool shortended = false)
        {
            string text;
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if (shortended)
            {
                text = String.Join(".", version.Major.ToString(), version.Minor.ToString());
            }
            else
            {
                text = version.ToString();
            }

            return text;
        }

        public static string GetProgramInfo()
        {
            string text;

            var versionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
            var name = versionInfo.InternalName;
            var version = GetProgramVersion();
            var trademark = versionInfo.LegalTrademarks;

            text = String.Join(Environment.NewLine, new string[] { name, version, trademark });

            return text;
        }

        public static void PrintProgramInfo(System.IO.TextWriter output)
        {
            output.WriteLine(GetProgramInfo());
        }

        public static string GetHelp(OptionSet options)
        {
            string text;

            using (System.IO.TextWriter writer = new System.IO.StringWriter())
            {
                writer.WriteLine("Usage:");
                writer.WriteLine(GetProgramFilename());
                options.WriteOptionDescriptions(writer);

                text = writer.ToString();
            }

            return text;
        }

        public static void PrintHelp(OptionSet options, System.IO.TextWriter output)
        {
            output.WriteLine(GetHelp(options));
        }
    }
}
