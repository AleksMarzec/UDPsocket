using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Udp
{
    // Klasa odpowiedzialna za "serializację" komunikatu.
    // Konweruje obiekt Message na string i na odwrót.
    public class MessageSerializer
    {
        public static bool UseJson { get; } = false;
        private static Char KeyValueSeparator { get; } = '#';
        private static Char FieldSeparator { get; } = ' ';

        public static string Serialize(Message message)
        {
            if (UseJson)
            {
                return SerializeToJson(message);
            }
            else
            {
                return SerializeToCustom(message);
            }
        }

        public static Message Deserialize(string text)
        {
            if (UseJson)
            {
                return DeserializeFromJson(text);
            }
            else
            {
                return DeserializeFromCustom(text);
            }
        }

        public static string SerializeToJson(Message message)
        {
            string result;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            result = serializer.Serialize(message.Fields);

            return result;
        }

        public static Message DeserializeFromJson(string text)
        {
            Message result = new Message();
            var deserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            result = new Message(deserializer.Deserialize<Dictionary<string, string>>(text));

            return result;
        }

        public static string SerializeToCustom(Message message)
        {
            string result = String.Empty;
            List<string> fields = new List<string>();

            foreach (var value in message.Fields)
            {
                // TO DO
                //StringBuilder builder = new StringBuilder();
                //builder.Append(EscapeText(value.Key)).Append(MessageSerializer.KeyValueSeparator).Append(EscapeText(message.Fields[value.Value]));
                //fields.Add(builder.ToString());
                //fields.Add($"{EscapeText(value.Key)}{KeyValueSeparator.ToString()}{EscapeText(value.Value)}");
                fields.Add($"{value.Key}{KeyValueSeparator.ToString()}{value.Value}");
            }
            result = String.Join(MessageSerializer.FieldSeparator.ToString(), fields);

            return result;
        }

        public static Message DeserializeFromCustom(string text)
        {
            Message result = new Message();

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (string field in text.Split(MessageSerializer.FieldSeparator))
            {
                var keyvalue = field.Split(MessageSerializer.KeyValueSeparator);

                if (keyvalue.Length >= 2)
                {
                    dictionary[UnescapeText(keyvalue[0])] = UnescapeText(keyvalue[1]);
                }
            }
            result = new Message(dictionary);

            return result;
        }

        // Źródło: https://stackoverflow.com/a/31362213
        public static string EscapeText(string text)
        {
            return System.Uri.EscapeDataString(text);
        }

        // Źródło: https://stackoverflow.com/a/31362213
        public static string UnescapeText(string text)
        {
            return System.Uri.UnescapeDataString(text);
        }
    }
}
