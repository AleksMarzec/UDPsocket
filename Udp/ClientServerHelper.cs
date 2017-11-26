using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udp
{
    public static class ClientServerHelper
    {
        public static string GetTimeStamp()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
            return timestamp;
        }
    }
}
