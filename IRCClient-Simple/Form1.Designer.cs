namespace IRCClient_Simple
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ircTextBox = new System.Windows.Forms.TextBox();
            this.ServerInput = new System.Windows.Forms.TextBox();
            this.ServerName = new System.Windows.Forms.Label();
            this.ChannelsInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PortInput = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.UsernameInput = new System.Windows.Forms.TextBox();
            this.IRCRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 418);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(638, 20);
            this.textBox1.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(656, 242);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(132, 20);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ircTextBox
            // 
            this.ircTextBox.Location = new System.Drawing.Point(12, 12);
            this.ircTextBox.Multiline = true;
            this.ircTextBox.Name = "ircTextBox";
            this.ircTextBox.ReadOnly = true;
            this.ircTextBox.Size = new System.Drawing.Size(638, 386);
            this.ircTextBox.TabIndex = 2;
            // 
            // ServerInput
            // 
            this.ServerInput.Location = new System.Drawing.Point(656, 28);
            this.ServerInput.Name = "ServerInput";
            this.ServerInput.Size = new System.Drawing.Size(132, 20);
            this.ServerInput.TabIndex = 3;
            // 
            // ServerName
            // 
            this.ServerName.AutoSize = true;
            this.ServerName.Location = new System.Drawing.Point(702, 12);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(38, 13);
            this.ServerName.TabIndex = 4;
            this.ServerName.Text = "Server";
            // 
            // ChannelsInput
            // 
            this.ChannelsInput.Location = new System.Drawing.Point(656, 80);
            this.ChannelsInput.Name = "ChannelsInput";
            this.ChannelsInput.Size = new System.Drawing.Size(132, 20);
            this.ChannelsInput.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(697, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Channels";
            // 
            // PortInput
            // 
            this.PortInput.Location = new System.Drawing.Point(656, 134);
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(132, 20);
            this.PortInput.TabIndex = 7;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(705, 115);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(26, 13);
            this.PortLabel.TabIndex = 8;
            this.PortLabel.Text = "Port";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(694, 171);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 9;
            this.UsernameLabel.Text = "Username";
            // 
            // UsernameInput
            // 
            this.UsernameInput.Location = new System.Drawing.Point(656, 188);
            this.UsernameInput.Name = "UsernameInput";
            this.UsernameInput.Size = new System.Drawing.Size(132, 20);
            this.UsernameInput.TabIndex = 10;
            // 
            // IRCRichTextBox
            // 
            this.IRCRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.IRCRichTextBox.Name = "IRCRichTextBox";
            this.IRCRichTextBox.Size = new System.Drawing.Size(638, 386);
            this.IRCRichTextBox.TabIndex = 11;
            this.IRCRichTextBox.Text = string.Empty;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(656, 418);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(132, 19);
            this.SendMessageButton.TabIndex = 12;
            this.SendMessageButton.Text = "Send";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.IRCRichTextBox);
            this.Controls.Add(this.UsernameInput);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.PortInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChannelsInput);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.ServerInput);
            this.Controls.Add(this.ircTextBox);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "TheIRCClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox ircTextBox;
        private System.Windows.Forms.TextBox ServerInput;
        private System.Windows.Forms.Label ServerName;
        private System.Windows.Forms.TextBox ChannelsInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PortInput;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox UsernameInput;
        private System.Windows.Forms.RichTextBox IRCRichTextBox;
        private System.Windows.Forms.Button SendMessageButton;
    }
}

