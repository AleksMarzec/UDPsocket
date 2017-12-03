using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Udp;

namespace Client
{
    public partial class GUI : Form
    {
        private string IpAddressString { get; set; } = "127.0.0.1";
        private System.Net.IPAddress IpAddress { get; set; }
        private int Port { get; set; } = 8888;
        private ClientUdp Client { get; set; }
        private bool Connected { get; set; }
        private bool Max3Numbers { get; set; }



        public GUI(string ipaddress = "127.0.0.1", int port = 8888)
        {
            InitializeComponent();
            this.IpAddressString = ipaddress;
            this.IpAddress = System.Net.IPAddress.Parse(this.IpAddressString);
            this.Port = port;
            this.Connected = false;
            this.Max3Numbers = false;
        }


        private void PolaczButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortTextBox.Text != this.Port.ToString())
                {
                    ParseUserInputPort();
                }

                if (IpAddressTextBox.Text != this.IpAddressString)
                {
                    ParseUserInputIpAddress();
                }

                if (this.Connected)
                {
                    DialogResult dialogResult = MessageBox.Show("Czy na pewno chcesz zresetować sesję?\nWszystkie dane umieszczone na serwerze zostaną usuniete.", "Ostrzeżenie", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }

                this.Client = new ClientUdp(this.IpAddress, this.Port);
                this.Connected = true;
                Tuple<string, string> packetsTuple = this.Client.Connect();

                OstatnieKomunikatyWypiszTextBox.Text = packetsTuple.Item1 + Environment.NewLine + packetsTuple.Item2;

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

        private void WyslijButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Connected)
                {
                    MessageBox.Show("Musisz być połączony z serwerem, aby wykonać operacje.", "Błąd połączenia");
                    return;
                }

                OperationCommand parameters = new OperationCommand();
                parameters.ParseInputNums(LiczbyTextBox.Text);
                if (Max3Numbers && parameters.NumsLength != 3)
                {
                    MessageBox.Show("Musisz wysłać dokładnie 3 liczby.\n" +
                        "Odznacz opcję automatycznego wysyłania finalnego wyniku po 3 komunikatach jeśli chcesz kontynuować.", "Błąd wysyłania");
                    return;
                }

                if (string.IsNullOrEmpty(OperacjaComboBox.Text))
                {
                    MessageBox.Show("Przed wysłaniem wybierz operację na liczbach jaką chcesz wykonać.", "Błąd operacji");
                    return;
                }

                parameters.ParseInputOperation(OperacjaComboBox.Text.ToString());

                if (OstatniKomunikatCheckBox.Checked)
                {
                    parameters.End = true;
                }
                else
                {
                    parameters.End = false;
                }

                if (Max3Numbers)
                {
                    parameters.End = true;
                }

                Tuple<string, string, string> packetsTuple = this.Client.Run(parameters);

                OstatnieKomunikatyWypiszTextBox.Text = packetsTuple.Item1 + Environment.NewLine + packetsTuple.Item2;
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

        private void RozlaczButton_Click(object sender, EventArgs e)
        {
            if (!this.Connected)
            {
                MessageBox.Show("Nie jesteś połączony z serwerem.");
                return;
            }
            else
            {
                Tuple<string, string> packetsTuple = this.Client.EndConnection();
                OstatnieKomunikatyWypiszTextBox.Text = packetsTuple.Item1 + Environment.NewLine + packetsTuple.Item2;
                this.Connected = false;
            }
        }

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

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyjść z aplikacji?\nJeśli jesteś połączony z serwerem zostaniesz automatycznie rozłączony.", "Zamykanie aplikacji", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }

            if (Connected)
            {
                this.RozlaczButton_Click(sender, e);
                this.Connected = false;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Udp.Program.GetProgramName() + Udp.Program.GetProgramVersion() + Udp.Program.GetProgramInfo());
        }

        private void LiczbyTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
