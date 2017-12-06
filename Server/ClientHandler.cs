using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udp;

namespace Server
{
    public class ClientHandler : Udp.ClientServerHandler
    {
        public string Operation { get; protected set; } = null;
        public List<int> Nums { get; protected set; }
        public int NumsCounter { get; set; }
        public bool End { get; set; }

        public ClientHandler() : base() { }

        // Resets session
        protected override void SessionRestart(string session = null)
        {
            base.SessionRestart(session);
            this.Operation = null;
            this.Nums = new List<int>();
        }

        public Message ProceedRequest(Message request)
        {
            Message message = CreateMessage();

            if (request == null || request.Fields == null)
            {
                // Error
                message.Fields["blad"] = "1";
            }
            else
            {
                if (request.Fields.Count <= 0)
                {
                    message.Fields["blad"] = "2";
                }
                else
                {
                    string identifier;
                    request.Fields.TryGetValue(ProtocolStrings.SessionField, out identifier);

                    // Other identifier restars session
                    if (identifier != this.Session)
                    {
                        SessionRestart(identifier);

                        // Changes identifier
                        if (this.Session == null)
                        {
                            message.Fields.Remove(identifier);
                        }
                        else
                        {
                            message.Fields[ProtocolStrings.SessionField] = this.Session;
                        }
                    }

                    // Licznik, identyfikator operacji
                    string counter = null;
                    request.Fields.TryGetValue(ProtocolStrings.CounterField, out counter);

                    // Przepisuje identyfikator operacji jeśli jest różny od null
                    if (counter != null)
                    {
                        message.Fields[ProtocolStrings.CounterField] = counter;
                    }

                    // Pole statusu. Definiuję wykonywaną komendę.
                    // Przykłady: start sesji, kończenie sesji, wykonywanie obliczeń.
                    string status;
                    request.Fields.TryGetValue(ProtocolStrings.RequestField, out status);

                    if (status != null && status == ProtocolStrings.RequestFieldHelloAction)
                    {
                        message.Fields[ProtocolStrings.ResponseField] = ProtocolStrings.ResponseFieldHelloAction;
                        SessionRestart(identifier);
                    }

                    // Pole operatora
                    // Definiuje operację która ma zostać przeprowadzona na argumentach liczbowych.
                    string operation = null;
                    request.Fields.TryGetValue(ProtocolStrings.OperationField, out operation);
                    if (operation != null)
                    {
                        if (operation != this.Operation)
                        {
                            this.Nums.Clear();
                        }
                        this.Operation = operation;
                    }

                    GetResponseEnd(request);

                    // Handling num values
                    string numsCounterString;
                    request.Fields.TryGetValue(ProtocolStrings.ArgCounterField, out numsCounterString);
                    if (numsCounterString != null)
                    {
                        this.NumsCounter = int.Parse(numsCounterString);
                    }

                    for (int i = 0; i < this.NumsCounter; i++)
                    {
                        string numString;
                        request.Fields.TryGetValue($"{ProtocolStrings.ArgField}{i + 1}".ToString(), out numString);

                        if (numString != null)
                        {
                            int num = 0;
                            if (int.TryParse(numString, out num))
                            {
                                this.Nums.Add(num);
                            }
                        }
                    }

                    if (status != null && status == ProtocolStrings.RequestFieldOperationAction)
                    {
                        message.Fields[ProtocolStrings.ResponseField] = ProtocolStrings.ResponseFieldResultAction;

                        string ok = "0";
                        int result = 0;

                        if (this.Nums != null)
                        {
                            if (this.Operation != null)
                            {
                                string operationparse = this.Operation.ToLower();

                                ok = "1";

                                if (operationparse == "dodawanie")
                                {
                                    result = Addition();
                                }
                                else if (operationparse == "mnozenie")
                                {
                                    result = Multiplication();
                                }
                                else if (operationparse == "bitowelub")
                                {
                                    result = BitwiseOr();
                                }
                                else if (operationparse == "bitowei")
                                {
                                    result = BitwiseAnd();
                                }
                                else
                                {
                                    ok = "0";
                                }
                            }
                        }

                        message.Fields[ProtocolStrings.StatusField] = ok;
                        message.Fields[ProtocolStrings.ResultField] = result.ToString();
                    }

                    if (this.End == true)
                    {
                        this.Nums.Clear();
                    }

                    if (status != null && status == ProtocolStrings.RequestFieldGoodbyeAction)
                    {
                        // Wymusza zakończenie sesji
                        message.Fields[ProtocolStrings.ResponseField] = ProtocolStrings.ResponseFieldGoodbyeAction;
                        SessionRestart(identifier);
                    }
                }
            }
            return message;
        }

        private void GetResponseEnd(Message request)
        {
            string endString;
            request.Fields.TryGetValue("end", out endString);
            if (endString != null)
            {
                this.End = bool.Parse(endString);
            }
        }

        private int Addition()
        {
            int result = 0;

            foreach (var arg in this.Nums)
            {
                result += arg;
            }
            return result;
        }

        private int Multiplication()
        {
            int result = 1;

            foreach (var arg in this.Nums)
            {
                result *= arg;
            }
            return result;
        }

        private int BitwiseOr()
        {
            int result = 0;

            foreach (var arg in this.Nums)
            {
                result |= arg;
            }

            return result;
        }

        private int BitwiseAnd()
        {
            int result = 1;

            foreach (var arg in this.Nums)
            {
                result &= arg;
            }

            return result;
        }
    }
}
