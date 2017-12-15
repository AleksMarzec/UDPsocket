using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Udp;

namespace Client
{
    public class ClientUdp : Udp.Udp
    {
        private byte[] data { get; set; }
        IPEndPoint ipep;
        UdpClient socket { get; set; }
        ServerHandler server { get; set; }

        public ClientUdp(System.Net.IPAddress address, int port) : base(address, port)
        {
            data = new byte[0];

            // Adres IP i numer portu serwera.
            ipep = new IPEndPoint(this.Address, this.Port); 

            // Ustanawiamy gniazdo typu datagramowego z wykorzystaniem protokołu UDP (klasa pomocnicza UdpClient).
            socket = new UdpClient(ipep.Address.ToString(), ipep.Port); // Nie musimy wywoływać metody Connect, ponieważ używamy konstruktora, który dba o kojarzenie gniazda z docelowym adresem i portem.

            // Inicjalizacja obiektu do obsługi wymiany komunikatów z serwerem. 
            // Nadawany jest tutaj identyfikator sesji.
            server = new ServerHandler(Guid.NewGuid().ToString()); // Pomysł na uniwersalny identyfikator zaczerpnięty z https://stackoverflow.com/questions/11313205/generate-a-unique-id
        }

        // - socket
        // - connect

        // Rozpoczyna sesję. 
        // Wysyła wiadomość powitalną do serwera.
        public Tuple<string, string> Connect()
        {
            // Rozpoczęcie transmisji.
            Message messageBegin = server.CreateMessageBegin(); // Utworzenie komunikatu powitalnego - nie wymaga interwencji użytkownika.
            string messageBeginString = MessageSerializer.Serialize(messageBegin); //Serializacja standardowego komunikatu powitalnego.
            data = Encoding.ASCII.GetBytes(messageBeginString); 

            socket.Send(data, data.Length); // Wysyłanie.
            string sended = ($"> {ipep.ToString()}: {Encoding.ASCII.GetString(data)}");

            data = socket.Receive(ref ipep); // Odbieranie odpowiedzi serwera. Dane zostają umieszczone w tablicy bajtów.
            string stringData = Encoding.ASCII.GetString(data, 0, data.Length);
            string received = ($"< {ipep.ToString()}: {stringData}");

            return new Tuple<string, string>(sended, received);
        }

        // Wysyła żądania do serwera i obsługuje odpowiedzi.
        // Zwraca wysłany komunikat, odebrany komunikat od serwera i odpowiedź. 
        public Tuple<string, string, string> Run(OperationCommand Parameters)
        {
            // Tworzenie wiadomości na podstawie danych podanych przez użytkownika.
            Message message = server.CreateMessageRequest(Parameters);
            string messageString = MessageSerializer.Serialize(message); // Serializacja komunikatu do formy z zadania klucz#wartość.

            data = Encoding.ASCII.GetBytes(messageString);

            // Komunikat zostaje wysłany jako pojedynczy pakiet. W przypadku klasy UdpSocket nie ma potrzeby jawnego przekazywania punktu końcowego
            // jeśli został zdefiniowany w konstruktorze.
            socket.Send(data, data.Length); 
            string sended = ($"> {ipep.ToString()}: {Encoding.ASCII.GetString(data)}");

            data = socket.Receive(ref ipep);
            string stringData = Encoding.ASCII.GetString(data, 0, data.Length);
            string received = ($"< {ipep.ToString()}: {stringData}");

            // Obsługa odpowiedzi.
            Message response = MessageSerializer.Deserialize(stringData); // Deserializacja odpowiedzi od serwera.
            string responsedata = "blad";
            if (response != null && response.Fields != null)
            {
                response.Fields.TryGetValue(ProtocolStrings.ResultField, out responsedata);
            }

            return new Tuple<string, string, string>(sended, received, responsedata);
        }

        // Kończy sesję.
        // Zwraca wysłany przez użytkownika komunikat i odpowiedź serwera.
        public Tuple<string, string> EndConnection()
        {
            // Tworzy komunikat informaujący serwer o zakończeniu sesji - nie wymaga interwencji użytkownika.
            Message messageEnd = server.CreateMessageEnd();
            string messageEndString = MessageSerializer.Serialize(messageEnd);
            data = Encoding.ASCII.GetBytes(messageEndString);
            string sended = ($"> {ipep.ToString()}: {Encoding.ASCII.GetString(data)}");

            socket.Send(data, data.Length);
            data = socket.Receive(ref ipep);

            string stringData = Encoding.ASCII.GetString(data, 0, data.Length);

            string received = ($"< {ipep.ToString()}: {stringData}");

            socket.Close();
            return new Tuple<string, string>(sended, received);
        }

    }
}
