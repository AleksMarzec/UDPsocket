namespace Server
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
            this.LabelIloscPolaczonychKlientow = new System.Windows.Forms.Label();
            this.LabelIloscPolaczonychKlientowLiczba = new System.Windows.Forms.Label();
            this.LabelKomunikaty = new System.Windows.Forms.Label();
            this.RichTextBoxKomunikaty = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelPort = new System.Windows.Forms.Label();
            this.TextBoxPort = new System.Windows.Forms.TextBox();
            this.ButtonPolacz = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelIloscPolaczonychKlientow
            // 
            this.LabelIloscPolaczonychKlientow.AutoSize = true;
            this.LabelIloscPolaczonychKlientow.Location = new System.Drawing.Point(13, 124);
            this.LabelIloscPolaczonychKlientow.Name = "LabelIloscPolaczonychKlientow";
            this.LabelIloscPolaczonychKlientow.Size = new System.Drawing.Size(139, 13);
            this.LabelIloscPolaczonychKlientow.TabIndex = 0;
            this.LabelIloscPolaczonychKlientow.Text = "Ilość połączonych klientów:";
            // 
            // LabelIloscPolaczonychKlientowLiczba
            // 
            this.LabelIloscPolaczonychKlientowLiczba.AutoSize = true;
            this.LabelIloscPolaczonychKlientowLiczba.Location = new System.Drawing.Point(158, 124);
            this.LabelIloscPolaczonychKlientowLiczba.Name = "LabelIloscPolaczonychKlientowLiczba";
            this.LabelIloscPolaczonychKlientowLiczba.Size = new System.Drawing.Size(13, 13);
            this.LabelIloscPolaczonychKlientowLiczba.TabIndex = 1;
            this.LabelIloscPolaczonychKlientowLiczba.Text = "0";
            // 
            // LabelKomunikaty
            // 
            this.LabelKomunikaty.AutoSize = true;
            this.LabelKomunikaty.Location = new System.Drawing.Point(13, 167);
            this.LabelKomunikaty.Name = "LabelKomunikaty";
            this.LabelKomunikaty.Size = new System.Drawing.Size(65, 13);
            this.LabelKomunikaty.TabIndex = 2;
            this.LabelKomunikaty.Text = "Komunikaty:";
            this.LabelKomunikaty.Click += new System.EventHandler(this.label3_Click);
            // 
            // RichTextBoxKomunikaty
            // 
            this.RichTextBoxKomunikaty.Location = new System.Drawing.Point(16, 195);
            this.RichTextBoxKomunikaty.Name = "RichTextBoxKomunikaty";
            this.RichTextBoxKomunikaty.ReadOnly = true;
            this.RichTextBoxKomunikaty.Size = new System.Drawing.Size(731, 136);
            this.RichTextBoxKomunikaty.TabIndex = 3;
            this.RichTextBoxKomunikaty.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(782, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panelToolStripMenuItem
            // 
            this.panelToolStripMenuItem.Name = "panelToolStripMenuItem";
            this.panelToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.panelToolStripMenuItem.Text = "Panel";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // LabelPort
            // 
            this.LabelPort.AutoSize = true;
            this.LabelPort.Location = new System.Drawing.Point(13, 61);
            this.LabelPort.Name = "LabelPort";
            this.LabelPort.Size = new System.Drawing.Size(29, 13);
            this.LabelPort.TabIndex = 5;
            this.LabelPort.Text = "Port:";
            // 
            // TextBoxPort
            // 
            this.TextBoxPort.Location = new System.Drawing.Point(58, 58);
            this.TextBoxPort.Name = "TextBoxPort";
            this.TextBoxPort.Size = new System.Drawing.Size(100, 20);
            this.TextBoxPort.TabIndex = 6;
            this.TextBoxPort.Text = "8888";
            // 
            // ButtonPolacz
            // 
            this.ButtonPolacz.Location = new System.Drawing.Point(355, 80);
            this.ButtonPolacz.Name = "ButtonPolacz";
            this.ButtonPolacz.Size = new System.Drawing.Size(337, 23);
            this.ButtonPolacz.TabIndex = 7;
            this.ButtonPolacz.Text = "Załącz serwer!!!!!";
            this.ButtonPolacz.UseVisualStyleBackColor = true;
            this.ButtonPolacz.Click += new System.EventHandler(this.ButtonPolacz_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 516);
            this.Controls.Add(this.ButtonPolacz);
            this.Controls.Add(this.TextBoxPort);
            this.Controls.Add(this.LabelPort);
            this.Controls.Add(this.RichTextBoxKomunikaty);
            this.Controls.Add(this.LabelKomunikaty);
            this.Controls.Add(this.LabelIloscPolaczonychKlientowLiczba);
            this.Controls.Add(this.LabelIloscPolaczonychKlientow);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI";
            this.Text = "Server";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelIloscPolaczonychKlientow;
        private System.Windows.Forms.Label LabelIloscPolaczonychKlientowLiczba;
        private System.Windows.Forms.Label LabelKomunikaty;
        private System.Windows.Forms.RichTextBox RichTextBoxKomunikaty;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem panelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.Label LabelPort;
        private System.Windows.Forms.TextBox TextBoxPort;
        private System.Windows.Forms.Button ButtonPolacz;
    }
}

