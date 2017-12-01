namespace Client
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.IpAddressLabel = new System.Windows.Forms.Label();
            this.IpAddressTextBox = new System.Windows.Forms.TextBox();
            this.OperacjaLabel = new System.Windows.Forms.Label();
            this.OperacjaComboBox = new System.Windows.Forms.ComboBox();
            this.LiczbyLabel = new System.Windows.Forms.Label();
            this.LiczbyTextBox = new System.Windows.Forms.TextBox();
            this.OstatniKomunikatCheckBox = new System.Windows.Forms.CheckBox();
            this.WyslijButton = new System.Windows.Forms.Button();
            this.WynikLabel = new System.Windows.Forms.Label();
            this.WynikOdpowiedzLabel = new System.Windows.Forms.Label();
            this.OstatnieKomunikatyLabel = new System.Windows.Forms.Label();
            this.OstatnieKomunikatyWypiszLabel = new System.Windows.Forms.Label();
            this.PolaczButton = new System.Windows.Forms.Button();
            this.RozlaczButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(921, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panelToolStripMenuItem
            // 
            this.panelToolStripMenuItem.Name = "panelToolStripMenuItem";
            this.panelToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.panelToolStripMenuItem.Text = "Panel";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(12, 45);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 13);
            this.PortLabel.TabIndex = 1;
            this.PortLabel.Text = "Port:";
            this.PortLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(89, 42);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(100, 20);
            this.PortTextBox.TabIndex = 2;
            this.PortTextBox.Text = "8888";
            // 
            // IpAddressLabel
            // 
            this.IpAddressLabel.AutoSize = true;
            this.IpAddressLabel.Location = new System.Drawing.Point(12, 81);
            this.IpAddressLabel.Name = "IpAddressLabel";
            this.IpAddressLabel.Size = new System.Drawing.Size(50, 13);
            this.IpAddressLabel.TabIndex = 3;
            this.IpAddressLabel.Text = "Adres IP:";
            this.IpAddressLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // IpAddressTextBox
            // 
            this.IpAddressTextBox.Location = new System.Drawing.Point(89, 78);
            this.IpAddressTextBox.Name = "IpAddressTextBox";
            this.IpAddressTextBox.Size = new System.Drawing.Size(100, 20);
            this.IpAddressTextBox.TabIndex = 4;
            this.IpAddressTextBox.Text = "127.0.0.1";
            // 
            // OperacjaLabel
            // 
            this.OperacjaLabel.AutoSize = true;
            this.OperacjaLabel.Location = new System.Drawing.Point(12, 179);
            this.OperacjaLabel.Name = "OperacjaLabel";
            this.OperacjaLabel.Size = new System.Drawing.Size(53, 13);
            this.OperacjaLabel.TabIndex = 5;
            this.OperacjaLabel.Text = "Operacja:";
            this.OperacjaLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // OperacjaComboBox
            // 
            this.OperacjaComboBox.FormattingEnabled = true;
            this.OperacjaComboBox.Items.AddRange(new object[] {
            "dodawanie",
            "mnożenie",
            "logiczne lub",
            "logiczne i"});
            this.OperacjaComboBox.Location = new System.Drawing.Point(89, 176);
            this.OperacjaComboBox.Name = "OperacjaComboBox";
            this.OperacjaComboBox.Size = new System.Drawing.Size(121, 21);
            this.OperacjaComboBox.TabIndex = 6;
            // 
            // LiczbyLabel
            // 
            this.LiczbyLabel.AutoSize = true;
            this.LiczbyLabel.Location = new System.Drawing.Point(12, 224);
            this.LiczbyLabel.Name = "LiczbyLabel";
            this.LiczbyLabel.Size = new System.Drawing.Size(40, 13);
            this.LiczbyLabel.TabIndex = 7;
            this.LiczbyLabel.Text = "Liczby:";
            // 
            // LiczbyTextBox
            // 
            this.LiczbyTextBox.Location = new System.Drawing.Point(89, 221);
            this.LiczbyTextBox.Name = "LiczbyTextBox";
            this.LiczbyTextBox.Size = new System.Drawing.Size(179, 20);
            this.LiczbyTextBox.TabIndex = 8;
            // 
            // OstatniKomunikatCheckBox
            // 
            this.OstatniKomunikatCheckBox.AutoSize = true;
            this.OstatniKomunikatCheckBox.Location = new System.Drawing.Point(306, 224);
            this.OstatniKomunikatCheckBox.Name = "OstatniKomunikatCheckBox";
            this.OstatniKomunikatCheckBox.Size = new System.Drawing.Size(111, 17);
            this.OstatniKomunikatCheckBox.TabIndex = 9;
            this.OstatniKomunikatCheckBox.Text = "Ostatni komunikat";
            this.OstatniKomunikatCheckBox.UseVisualStyleBackColor = true;
            // 
            // WyslijButton
            // 
            this.WyslijButton.Location = new System.Drawing.Point(89, 310);
            this.WyslijButton.Name = "WyslijButton";
            this.WyslijButton.Size = new System.Drawing.Size(259, 23);
            this.WyslijButton.TabIndex = 10;
            this.WyslijButton.Text = "Wyślij";
            this.WyslijButton.UseVisualStyleBackColor = true;
            this.WyslijButton.Click += new System.EventHandler(this.WyslijButton_Click);
            // 
            // WynikLabel
            // 
            this.WynikLabel.AutoSize = true;
            this.WynikLabel.Location = new System.Drawing.Point(15, 386);
            this.WynikLabel.Name = "WynikLabel";
            this.WynikLabel.Size = new System.Drawing.Size(40, 13);
            this.WynikLabel.TabIndex = 11;
            this.WynikLabel.Text = "Wynik:";
            // 
            // WynikOdpowiedzLabel
            // 
            this.WynikOdpowiedzLabel.AutoSize = true;
            this.WynikOdpowiedzLabel.Location = new System.Drawing.Point(57, 386);
            this.WynikOdpowiedzLabel.Name = "WynikOdpowiedzLabel";
            this.WynikOdpowiedzLabel.Size = new System.Drawing.Size(16, 13);
            this.WynikOdpowiedzLabel.TabIndex = 12;
            this.WynikOdpowiedzLabel.Text = "---";
            // 
            // OstatnieKomunikatyLabel
            // 
            this.OstatnieKomunikatyLabel.AutoSize = true;
            this.OstatnieKomunikatyLabel.Location = new System.Drawing.Point(508, 42);
            this.OstatnieKomunikatyLabel.Name = "OstatnieKomunikatyLabel";
            this.OstatnieKomunikatyLabel.Size = new System.Drawing.Size(106, 13);
            this.OstatnieKomunikatyLabel.TabIndex = 13;
            this.OstatnieKomunikatyLabel.Text = "Ostatnie komunikaty:";
            // 
            // OstatnieKomunikatyWypiszLabel
            // 
            this.OstatnieKomunikatyWypiszLabel.AutoSize = true;
            this.OstatnieKomunikatyWypiszLabel.Location = new System.Drawing.Point(508, 78);
            this.OstatnieKomunikatyWypiszLabel.Name = "OstatnieKomunikatyWypiszLabel";
            this.OstatnieKomunikatyWypiszLabel.Size = new System.Drawing.Size(16, 13);
            this.OstatnieKomunikatyWypiszLabel.TabIndex = 14;
            this.OstatnieKomunikatyWypiszLabel.Text = "---";
            // 
            // PolaczButton
            // 
            this.PolaczButton.Location = new System.Drawing.Point(89, 127);
            this.PolaczButton.Name = "PolaczButton";
            this.PolaczButton.Size = new System.Drawing.Size(259, 23);
            this.PolaczButton.TabIndex = 15;
            this.PolaczButton.Text = "Połącz";
            this.PolaczButton.UseVisualStyleBackColor = true;
            this.PolaczButton.Click += new System.EventHandler(this.PolaczButton_Click);
            // 
            // RozlaczButton
            // 
            this.RozlaczButton.Location = new System.Drawing.Point(511, 310);
            this.RozlaczButton.Name = "RozlaczButton";
            this.RozlaczButton.Size = new System.Drawing.Size(256, 23);
            this.RozlaczButton.TabIndex = 16;
            this.RozlaczButton.Text = "Rozłącz";
            this.RozlaczButton.UseVisualStyleBackColor = true;
            this.RozlaczButton.Click += new System.EventHandler(this.RozlaczButton_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 463);
            this.Controls.Add(this.RozlaczButton);
            this.Controls.Add(this.PolaczButton);
            this.Controls.Add(this.OstatnieKomunikatyWypiszLabel);
            this.Controls.Add(this.OstatnieKomunikatyLabel);
            this.Controls.Add(this.WynikOdpowiedzLabel);
            this.Controls.Add(this.WynikLabel);
            this.Controls.Add(this.WyslijButton);
            this.Controls.Add(this.OstatniKomunikatCheckBox);
            this.Controls.Add(this.LiczbyTextBox);
            this.Controls.Add(this.LiczbyLabel);
            this.Controls.Add(this.OperacjaComboBox);
            this.Controls.Add(this.OperacjaLabel);
            this.Controls.Add(this.IpAddressTextBox);
            this.Controls.Add(this.IpAddressLabel);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI";
            this.Text = "GUI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem panelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label IpAddressLabel;
        private System.Windows.Forms.TextBox IpAddressTextBox;
        private System.Windows.Forms.Label OperacjaLabel;
        private System.Windows.Forms.ComboBox OperacjaComboBox;
        private System.Windows.Forms.Label LiczbyLabel;
        private System.Windows.Forms.TextBox LiczbyTextBox;
        private System.Windows.Forms.CheckBox OstatniKomunikatCheckBox;
        private System.Windows.Forms.Button WyslijButton;
        private System.Windows.Forms.Label WynikLabel;
        private System.Windows.Forms.Label WynikOdpowiedzLabel;
        private System.Windows.Forms.Label OstatnieKomunikatyLabel;
        private System.Windows.Forms.Label OstatnieKomunikatyWypiszLabel;
        private System.Windows.Forms.Button PolaczButton;
        private System.Windows.Forms.Button RozlaczButton;
    }
}