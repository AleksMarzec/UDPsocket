using System;
using System.Collections.Generic;


namespace Udp
{
    public class MessageSerializer
    {
        public static string Serialize(Message message)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(message.Fields);
        }

        public static Message Deserialize(string text)
        {
            var deserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new Message(deserializer.Deserialize<Dictionary<string, string>>(text));
        }
    }
}
