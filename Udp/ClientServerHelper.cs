using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udp
{
    public static class ClientServerHelper
    {
        // Zwraca czas UTC w postaci znacznika czasu.
        public static string GetTimeStamp()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd/HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
            return timestamp;
        }

        // Zwraca int na podany enum i null w przypadku braku dopasowania
        public static TEnum? ToEnum<TEnum>(int i) where TEnum : struct
        {
            TEnum? result = null;
            if (Enum.IsDefined(typeof(TEnum), i))
            {
                result = (TEnum)System.Enum.ToObject(typeof(TEnum), i);
            }
            return result;
        }

        // Zamienia string na podany enum
        // Jeśli strint == true to traktuje podany napis s jako wartość int i wywołuje ToEnum<TEnum>(int i)
        // W przypadku braku dopasowania zwraca null
        public static TEnum? ToEnum<TEnum>(string s, bool strint = false) where TEnum : struct
        {
            TEnum? result = null;

            if (strint)
            {
                int i = 0;
                if (Int32.TryParse(s, out i))
                {
                    result = ToEnum<TEnum>(i);
                }
            }
            else
            {
                if (s != null)
                {
                    result = (TEnum)System.Enum.Parse(typeof(TEnum), s);
                }
            }

            return result;
        }
    }
}
