using System;
using System.Windows.Forms;
using Udp;

namespace Client
{
    public partial class GUI : Form
    {
        private string IpAddressString { get; set; }
        private System.Net.IPAddress IpAddress { get; set; }
        private int Port { get; set; }
        private ClientUdp Client { get; set; }
        private bool Connected { get; set; } // Zmienia wartość w zależności od akcji podejmwanych przez użytkownika.
        private bool Max3Numbers { get; set; } // Jeśli zaznaczona opcja wysyłania 3 liczb i odbierania ostatecznego wyniku to pole "end" przybiera automatycznie wartość true.



        public GUI(string ipaddress = "127.0.0.1", int port = 40000)
        {
            InitializeComponent();
            this.IpAddressString = ipaddress;
            this.IpAddress = System.Net.IPAddress.Parse(this.IpAddressString);
            this.Port = port;
            this.Connected = false;
            this.Max3Numbers = false;
        }

        // Obsługa przycisku "połącz".
        private void PolaczButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Sprawdzanie czy adres IP oraz port są takie same jak domyślnie wiadomości.
                // Jeśli nie - parsuje.
                if (PortTextBox.Text != this.Port.ToString())
                {
                    ParseUserInputPort();
                }

                if (IpAddressTextBox.Text != this.IpAddressString)
                {
                    ParseUserInputIpAddress();
                }

                // Obsługa błędu w przypadku, gdy użytkownik jest już połączony i ponownie wybiera "Połącz".
                if (this.Connected)
                {
                    DialogResult dialogResult = MessageBox.Show("Czy na pewno chcesz zresetować sesję?\nWszystkie dane umieszczone na serwerze zostaną usuniete.", "Ostrzeżenie", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }

                // Łączenie z serwerem. Wysłanie komunikatu powitalnego.
                this.Client = new ClientUdp(this.IpAddress, this.Port);
                this.Connected = true;
                Tuple<string, string> packetsTuple = this.Client.Connect();

                OstatnieKomunikatyWypiszTextBox.Text = packetsTuple.Item1 + Environment.NewLine + Environment.NewLine + packetsTuple.Item2;

            }
            catch (FormatException fEx)
            {
                MessageBox.Show(fEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Obsługa przycisku "wyślij".
        private void WyslijButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Obsługa błędu. Niepołączony klient chce wysłać wiadomość.
                if (!Connected)
                {
                    MessageBox.Show("Musisz być połączony z serwerem, aby wykonać operacje.", "Błąd połączenia");
                    return;
                }

                // Parsowanie danych podanych przez użytkownika.
                OperationCommand parameters = new OperationCommand();
                parameters.ParseInputNums(LiczbyTextBox.Text); // Liczby.

                // Obsługa błędu. Klient mając zaznaczoną opcję max 3 argumentów wpisuje ich więcej.
                if (Max3Numbers && parameters.NumsLength != 3)
                {
                    MessageBox.Show("Musisz wysłać dokładnie 3 liczby.\n" +
                        "Odznacz opcję automatycznego wysyłania finalnego wyniku po 3 komunikatach jeśli chcesz kontynuować.", "Błąd wysyłania");
                    return;
                }

                // Obsługa błęduw. Brak argumentów liczbowych.
                if (string.IsNullOrEmpty(OperacjaComboBox.Text))
                {
                    MessageBox.Show("Przed wysłaniem wybierz operację na liczbach jaką chcesz wykonać.", "Błąd operacji");
                    return;
                }

                parameters.ParseInputOperation(OperacjaComboBox.Text.ToString()); // Parsowanie operacji.

                if (OstatniKomunikatCheckBox.Checked || Max3Numbers) // Parsowanie - czy zwrócić wynik ostateczny.
                {
                    parameters.End = true;
                }
                else
                {
                    parameters.End = false;
                }

                Tuple<string, string, string> packetsTuple = this.Client.Run(parameters);

                OstatnieKomunikatyWypiszTextBox.Text = packetsTuple.Item1 + Environment.NewLine + Environment.NewLine + packetsTuple.Item2;
                WynikOdpowiedzLabel.Text = packetsTuple.Item3.ToString();
            }
            catch (FormatException fEx)
            {
                MessageBox.Show(fEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Obsługa przycisku "rozłącz".
        private void RozlaczButton_Click(object sender, EventArgs e)
        {
            // Obsługa błędu. Niepołączony klient chce rozłączyć się z serwerem.
            if (!this.Connected)
            {
                MessageBox.Show("Nie jesteś połączony z serwerem.");
                return;
            }
            else
            {
                Tuple<string, string> packetsTuple = this.Client.EndConnection();
                OstatnieKomunikatyWypiszTextBox.Text = packetsTuple.Item1 + Environment.NewLine + Environment.NewLine + packetsTuple.Item2;
                this.Connected = false;
            }
        }

        // Obsługa CheckBoxa max 3 argumentów.
        private void Domyslnie3LiczbyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Domyslnie3LiczbyCheckBox.Checked)
            {
                this.Max3Numbers = true;
            }
            else
            {
                this.Max3Numbers = false;
            }
        }

        // Parsowanie portu podanego przez klienta.
        private void ParseUserInputPort()
        {
            int port = 0;
            try
            {
                int.TryParse(PortTextBox.Text, out port);
            }
            catch (FormatException fEx)
            {
                MessageBox.Show("Port", fEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Port", ex.Message);
            }

            this.Port = port;
        }

        // Parsowanie adresu IP podanego przez klienta.
        private void ParseUserInputIpAddress()
        {
            try
            {
                this.IpAddress = System.Net.IPAddress.Parse(IpAddressTextBox.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adres IP", ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // Obsługa wyjścia z programu.
        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ostrzeżenie.
            if (MessageBox.Show("Czy na pewno chcesz wyjść z aplikacji?\nJeśli jesteś połączony z serwerem zostaniesz automatycznie rozłączony.", "Zamykanie aplikacji", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (this.Connected) // Jeśli klient połączny z serwerem - prawidłowo zakończ sesję. 
                {
                    this.RozlaczButton_Click(sender, e);
                    this.Connected = false;
                }
            }
        }

        // TODO
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uruchamianie wirus.exe");
            MessageBox.Show("Twój komputer został poprawnie shakowany.\nCała historia przeglądarki i DNSy zostały przesłane Karaczanowi.");
        }
    }
}
