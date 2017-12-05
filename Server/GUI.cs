using System;
using System.Windows.Forms;

namespace Server
{
    public partial class GUI : Form
    {
        private int ConnectedClients { get; set; }
        private System.Net.IPAddress IpAddress { get; set; }
        private int Port { get; set; }
        ServerUdp Server { get; set; }

        public GUI(int port = 8888)
        {
            this.ConnectedClients = 0;
            this.Port = port;
            this.IpAddress = System.Net.IPAddress.Any;
            this.Server = new ServerUdp(IpAddress, Port, this);
            InitializeComponent();
        }

        //TODO
        public void KomunikatyJoin(string text)
        {
            this.RichTextBoxKomunikaty.Text += $"{Environment.NewLine} {text}";
        }

        public void ClientConnected()
        {
            this.ConnectedClients++;
            this.LabelIloscPolaczonychKlientowLiczba.Text = this.ConnectedClients.ToString();
        }

        private void ButtonPolacz_Click(object sender, EventArgs e)
        {
            this.Server.Run();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


    }
}
