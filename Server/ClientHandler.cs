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
        public string Operation { get; protected set; } = null; // Typ operacji do wykonania na argumentach.
        public List<int> Nums { get; protected set; } // Lista argumentów.
        public int NumsCounter { get; set; } // Liczba arguemntów.
        public bool End { get; set; } // Czy ostatni komunikat z argumentami.


        public ClientHandler() : base() { }

        // Resetuje sesję.
        protected override void SessionRestart(string session = null)
        {
            base.SessionRestart(session);
            this.Operation = null;
            this.Nums = new List<int>();
        }

        // Obsługuje żądanie od klienta.
        public Message ProceedRequest(Message request)
        {
            string error = String.Empty;

            Message message = CreateMessage();

            // Niezdefiniowane zapytanie.
            if (request == null || request.Fields == null)
            {
                // Error
                message.Fields["blad"] = "1";
            }
            else
            {
                // Puste zapytanie.
                if (request.Fields.Count <= 0)
                {
                    message.Fields["blad"] = "2";
                }
                else
                {
                    string identifier; // Identyfikator sesji.
                    request.Fields.TryGetValue(ProtocolStrings.SessionField, out identifier);

                    // Inny identyfikator sesji powoduje restart sesji.
                    if (identifier != this.Session)
                    {
                        SessionRestart(identifier);

                        // Zmiana identyfikatora w nowym komunikacie.
                        if (this.Session == null)
                        {
                            message.Fields.Remove(identifier);
                        }
                        else
                        {
                            message.Fields[ProtocolStrings.SessionField] = this.Session;
                        }
                    }

                    // Licznik, identyfikator operacji.
                    string counter = null;
                    request.Fields.TryGetValue(ProtocolStrings.CounterField, out counter);

                    // Przepisuje identyfikator operacji jeśli jest różny od null.
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
                        // Przywitanie wymusza rozpoczęcie nowej sesji.
                        message.Fields[ProtocolStrings.ResponseField] = ProtocolStrings.ResponseFieldHelloAction;
                        SessionRestart(identifier);
                    }

                    // Parsowanie pola operatora.
                    // Definiuje operację która ma zostać przeprowadzona na argumentach liczbowych.
                    string operation = null;
                    request.Fields.TryGetValue(ProtocolStrings.OperationField, out operation);
                    if (operation != null)
                    {
                        // Inna operacja niż poprzednio. Wymusza wyczyszczenie całej listy argumentów.
                        if (operation != this.Operation)
                        {
                            this.Nums.Clear();
                        }
                        this.Operation = operation;
                    }

                    // Sprawdza czy komunikat z ostatnimi argumentami.
                    GetResponseEnd(request);

                    // Sprawdzanie ilości argumentów liczbowych w komunikacie.
                    // Pole przydatne do późniejszego zapisywania do listy samych liczb.
                    string numsCounterString;
                    request.Fields.TryGetValue(ProtocolStrings.ArgCounterField, out numsCounterString);
                    if (numsCounterString != null)
                    {
                        this.NumsCounter = int.Parse(numsCounterString);
                    }

                    // Parsowanie argumentów liczbowych.
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

                    // Obsługa żądania wykonania operacji.
                    if (status != null && status == ProtocolStrings.RequestFieldOperationAction)
                    {
                        message.Fields[ProtocolStrings.ResponseField] = ProtocolStrings.ResponseFieldResultAction;

                        string ok = "0"; // Do sprawdzenia czy operacja z komunikatu jest dozwolona.
                        int result = 0;

                        if (this.Nums != null)
                        {
                            if (this.Operation != null)
                            {
                                string operationparse = this.Operation.ToLower();

                                ok = "1";
                                try
                                {
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
                                catch (OverflowException oEx)
                                {
                                    ok = "0";
                                    error += $" error:{oEx.ToString()}";
                                }
                                catch (Exception Ex)
                                {
                                    ok = "0";
                                    error += $" error:{Ex.ToString()}";
                                }
                            }
                        }

                        message.Fields[ProtocolStrings.ErrorField] = error;
                        message.Fields[ProtocolStrings.StatusField] = ok;
                        message.Fields[ProtocolStrings.ResultField] = result.ToString();
                    }

                    // Jeśli otrzymano komunikat z żądaniem zwrócenia ostatecznego wyniku.
                    if (this.End == true)
                    {
                        // Czyszczenie listy argumentów.
                        this.Nums.Clear();
                    }

                    // Jeśli otrzymano komunikat z żądaniem zakończenia sesji.
                    if (status != null && status == ProtocolStrings.RequestFieldGoodbyeAction)
                    {
                        // Wymuszenie zakończenie sesji. Pożegnanie z klientem.
                        message.Fields[ProtocolStrings.ResponseField] = ProtocolStrings.ResponseFieldGoodbyeAction;
                        SessionRestart(identifier);
                    }
                }
            }
            return message;
        }

        // Parsuje pole "czy ostatni argument".
        private void GetResponseEnd(Message request)
        {
            string endString;
            request.Fields.TryGetValue(ProtocolStrings.End, out endString);
            if (endString != null)
            {
                // Jeśli true - zapamiętuje w zmiennej End i wymusza na końcu wyczyszczenie całej listy argumentów.
                this.End = bool.Parse(endString);
            }
        }

        // Obsługa operacji dodawania.
        private int Addition()
        {
            int result = 0;

            foreach (var arg in this.Nums)
            {
                result = checked(result + arg);
            }
            return result;
        }

        // Obsługa operacji mnożenia.
        private int Multiplication()
        {
            int result = 1;

            foreach (var arg in this.Nums)
            {
                result = checked(result* arg);
            }
            return result;
        }

        // Obsługa operacji bitowego "lub".
        private int BitwiseOr()
        {
            int result = 0;

            foreach (var arg in this.Nums)
            {
                result |= arg;
            }

            return result;
        }

        // Obsługa operacji bitowego "i".
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
