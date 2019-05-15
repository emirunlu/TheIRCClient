using System;
using System.Linq;
using System.Windows.Forms;

namespace IRCClient_Simple
{
    public partial class Form1 : Form
    {
        private IRCLogic _irc;

        public Form1()
        {
            _irc = new IRCLogic();
            TextBox.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
        }

        public void ConnectButton_Click(object sender, EventArgs e)
        {
            if (ServerInput.Text != string.Empty && PortLabel.Text != string.Empty && UsernameInput.Text != string.Empty && ChannelsInput.Text != string.Empty && _irc.IsClientRunning() == false)
            {
                int port = -1;
                if ((port = int.Parse(PortInput.Text)) != -1)
                {
                    //parameters as follows: ip or address to irc server, username, password(not functional), channels, and method to execute when message is received (see line 103)
                    _irc.SetupIRC(ServerInput.Text, UsernameInput.Text, ChannelsInput.Text, int.Parse(PortInput.Text));

                    //Sets event handlers for all the possible events (!IMPORTANT: do this after intializing irc.SetupIRC !!!)


                    _irc.StartClient();

                    ConnectButton.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("You need to fill in all the information fields!");
            }
        }
        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_irc.IsClientRunning())
            {
                _irc.StopClient();
            }
        }
    }
}
