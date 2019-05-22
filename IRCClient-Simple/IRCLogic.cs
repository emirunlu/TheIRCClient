using System;
using System.Linq;
using System.Threading;

namespace IRCClient_Simple
{
    class IRCLogic
    {
        public string NewIP { get; private set; }
        public int NewPort { get; private set; }
        public string NewUsername { get; private set; }
        public string NewPassword { get; private set; }
        public string NewChannels { get; private set; }
        public IRCClient IRCClient { get; private set; }

        public IRCLogic()
        {
            NewIP = string.Empty;
            NewPort = 0;
            NewUsername = string.Empty;
            NewPassword = string.Empty;
            NewChannels = string.Empty;
            IRCClient = new IRCClient();
        }

        public void SetupIRC(string ip, string username, string channels, int port = 0, string password = "", int timeout = 3000)
        {
            NewIP = ip;
            NewPort = port;
            NewUsername = username;
            NewPassword = password;
            NewChannels = channels;

            IRCClient.SetConnectionInformation(ip, username, channels, port, password, timeout);
        }

        public bool StartClient()
        {
            if (IRCClient != null)
            {
                if (!IRCClient.IsConnectionEstablished())
                {
                    IRCClient.Connect();

                    int timeout = 0;
                    while (!IRCClient.IsClientRunning())
                    {
                        Thread.Sleep(1);
                        if (timeout >= 3000)
                        {
                            return false;
                        }
                        timeout++;
                    }
                    return true;
                }
            }

            return false;
        }
        public bool IsClientRunning()
        {
            return IRCClient.IsClientRunning();
        }

        internal bool SendMessageToAll(string text)
        {
            return IRCClient.SendMessageToAll(text);
        }

        public bool StopClient()
        {
            //execute quit stuff
            bool check = false;

            check = IRCClient.StopClient();
            return check;
        }

    }
}
