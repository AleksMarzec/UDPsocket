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



        public GUI(string ipaddress = "127.0.0.1", int port = 8888)
        {
            InitializeComponent();
            this.IpAddressString = ipaddress;
            this.IpAddress = System.Net.IPAddress.Parse(this.IpAddressString);
            this.Port = port;
            this.Connected = false;
        }

        private void PolaczButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortTextBox.Text != this.Port.ToString())
                {
                    MessageBox.Show("xd");
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
                Tuple<string, string> packetsString = this.Client.Connect();

                OstatnieKomunikatyWypiszLabel.Text = packetsString.Item1 + Environment.NewLine + packetsString.Item2;

            }
            catch (FormatException fEx)
            {
                MessageBox.Show(fEx.Message);
                MessageBox.Show("polacz");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("polacz");
            }
        }

        private void WyslijButton_Click(object sender, EventArgs e)
        {
            try
            {
                OperationCommand parameters = new OperationCommand();
                parameters.ParseInput(LiczbyTextBox.Text); // exception for empty operation
                parameters.Operation = OperacjaComboBox.SelectedItem.ToString();

                this.Client.Run(parameters);

                if (OstatniKomunikatCheckBox.Checked)
                {
                    parameters.End = true;
                }
                else
                {
                    parameters.End = false;
                }
            }
            catch (FormatException fEx)
            {
                MessageBox.Show(fEx.Message);
                MessageBox.Show("wyslij");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("wyslij");
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
                this.Client.EndConnection();
                this.Connected = false;
            }
        }

        private void ParseUserInputPort()
        {
            int port = 0;
            try
            {
                if (int.TryParse(PortTextBox.Text, out port))
                {
                    MessageBox.Show("sparsowane");
                }
                else
                {
                    MessageBox.Show("niesparsowane");

                }
            }
            catch (FormatException fEx)
            {
                MessageBox.Show(fEx.Message);
                MessageBox.Show("port");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("port");
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
                MessageBox.Show(ex.Message);
                MessageBox.Show("adres");
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
    }
}
