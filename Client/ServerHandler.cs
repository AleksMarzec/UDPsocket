using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udp;

namespace Client
{
    public class ServerHandler : ClientServerHandler
    {
        public int Counter { get; protected set; } = 0;

        public ServerHandler(string session = null) : base(session)
        {
            this.Counter = 0;
        }

        protected override void SessionRestart(string session = null)
        {
            base.SessionRestart(session);
            this.Counter = 0;
        }

        public Message CreateMessageBegin()
        {
            return CreateMessageBegin(this.Session);
        }

        public Message CreateMessageBegin(string session)
        {
            SessionRestart(session);
            Message message = CreateMessageRequest();
            message.Fields["stat"] = "hsrv";
            return message;
        }

        public Message CreateMessageEnd()
        {
            Message message = CreateMessageRequest();
            SessionRestart(this.Session);
            message.Fields["stat"] = "byes";
            return message;
        }

        protected Message CreateMessageRequest()
        {
            this.Counter++;
            Message message = CreateMessage();
            message.Fields["licz"] = this.Counter.ToString();
            return message;
        }

        public Message CreateMessageRequest(OperationCommand cmd)
        {
            Message message = CreateMessageRequest();

            if (cmd != null)
            {
                message.Fields["oper"] = cmd.Operation;

                for (int i = 1; i <= cmd.Nums.Count; i++)
                {
                    message.Fields[$"num{i}"] = cmd.Nums.ElementAt(i - 1).ToString();
                }

                message.Fields["nums"] = cmd.NumsLength.ToString();
            }

            message.Fields["stat"] = "wynik";
            message.Fields["end"] = cmd.End.ToString();

            return message;
        }
    }
}
