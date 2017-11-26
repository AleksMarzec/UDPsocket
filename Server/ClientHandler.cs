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
        public List<int?> Nums { get; protected set; } = new List<int?>(new int?[3]);

        public ClientHandler() : base() { }

        // Resets session
        protected override void SessionRestart(string session = null)
        {
            base.SessionRestart(session);
            this.Operation = null;
            this.Nums = new List<int?>(new int?[3]);
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
                    request.Fields.TryGetValue("iden", out identifier);

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
                            message.Fields["iden"] = this.Session;
                        }
                    }

                    // Counter, identifier of operations
                    string counter;
                    request.Fields.TryGetValue("licz", out counter);

                    if (counter != null)
                    {
                        message.Fields["licz"] = counter;
                    }

                    // Status field. Defines executing command. 
                    // Session start, session end, doing some calculations.
                    string status;
                    request.Fields.TryGetValue("stat", out status);

                    if (status != null && status == "hsrv")
                    {
                        message.Fields["wiad"] = "husr";
                        SessionRestart(identifier);
                    }

                    // Operation field.
                    // Add values, multiply values, or, and.
                    string operation;
                    request.Fields.TryGetValue("oper", out operation);
                    if (operation != null)
                    {
                        this.Operation = operation;
                    }

                    // Handling num values
                    List<int?> nums = new List<int?>(new int?[3]);
                    for (int i = 0; i < 3; i++)
                    {
                        nums[i] = null;
                        string numString;
                        request.Fields.TryGetValue("num" + (i + 1).ToString(), out numString);

                        if (numString != null)
                        {
                            int num = 0;
                            if (int.TryParse(numString, out num))
                            {
                                nums[i] = num;
                            }
                        }

                        if (nums[i] != null)
                        {
                            this.Nums[i] = nums[i];
                        }
                    }

                    if (status != null && status == "wynik")
                    {
                        message.Fields["wiad"] = "result";
                        string ok = "0";
                        int result = 0;

                        if (this.Nums != null && this.Nums.Count >= 3 && this.Nums[0] != null && this.Nums[1] != null && this.Nums[2] != null)
                        {
                            if (this.Operation != null)
                            {
                                string operationparse = this.Operation.ToLower();

                                ok = "1";

                                if (operationparse == "mnozenie")
                                {
                                    result = this.Nums[0].Value * this.Nums[1].Value * this.Nums[2].Value;
                                }
                                else if (operationparse == "dodawanie")
                                {
                                    result = this.Nums[0].Value + this.Nums[1].Value + this.Nums[2].Value;
                                }
                                else if (operationparse == "lub")
                                {
                                    result = this.Nums[0].Value | this.Nums[1].Value | this.Nums[2].Value;
                                }
                                else if (operationparse == "i")
                                {
                                    result = this.Nums[0].Value & this.Nums[1].Value & this.Nums[2].Value;
                                }
                                else
                                {
                                    ok = "0";
                                }
                            }
                        }

                        message.Fields["okej"] = ok;
                        message.Fields["dane"] = result.ToString();
                    }

                    if (status != null && status == "byes")
                    {
                        message.Fields["wiad"] = "byeu";
                        SessionRestart(identifier);
                    }
                }
            }
            return message;
        }
    }
}
