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

        // Zamienia int na podany Enum. Zwraca null w przypadku braku dopasowania.
        // Źródła: https://stackoverflow.com/questions/4722010/enum-from-string-int-etc/4722240#4722240, 
        //         https://stackoverflow.com/questions/3811042/adding-constraints-for-nullable-enum/3811072#3811072
        public static TEnum? ToEnum<TEnum>(int i) where TEnum : struct
        {
            TEnum? result = null;
            if (Enum.IsDefined(typeof(TEnum), i))
            {
                result = (TEnum)System.Enum.ToObject(typeof(TEnum), i);
            }
            return result;
        }

        // Zamienia string na podany Enum.
        // Jeśli strint == true to traktuje podany napis s jako wartość int i wywołuje ToEnum<TEnum>(int i)
        // W przypadku braku dopasowania zwraca null.
        // Źródła: https://stackoverflow.com/questions/4722010/enum-from-string-int-etc/4722240#4722240,
        //         https://stackoverflow.com/questions/3811042/adding-constraints-for-nullable-enum/3811072#3811072
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
