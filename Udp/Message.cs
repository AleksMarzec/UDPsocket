using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udp
{
    // Klasa reprezentująca pojedynczy komunikat.
    public class Message
    {
        public Dictionary<string, string> Fields { get; set; }

        public Message()
        {
            this.Fields = new Dictionary<string, string>();
        }

        public Message(Dictionary<string, string> fields) : this()
        {
            foreach (var field in fields)
            {
                this.Fields[field.Key] = field.Value;
            }
        }

        public Message(Message message) : this(message.Fields) { }

    }
}
