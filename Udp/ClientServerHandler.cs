using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udp
{
    public abstract class ClientServerHandler
    {
        public string Session { get; protected set; } = null; // Nazwa sesji.

        public ClientServerHandler(string session = null)
        {
            this.Session = session;
        }

        // Resetowanie sesji.
        protected virtual void SessionRestart(string session = null)
        {
            this.Session = session;
        }

        protected Message CreateMessage()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary[ProtocolStrings.TimeField] = ClientServerHelper.GetTimeStamp();
            if (this.Session != null)
            {
                dictionary[ProtocolStrings.SessionField] = this.Session;
            }

            return new Message(dictionary);
        }
    }
}
