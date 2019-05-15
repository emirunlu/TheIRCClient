using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace IRCClient_Simple
{
    public class IRCClient
    {
        private string _newIp;
        private int _NewPort;
        private string _NewUsername;
        private string _newPassword;
        private string _NewChannels;
        private bool _isConnectionEstablised;
        private Task _receiverTask;
        private bool _IsClientRunning;
        private int _timeOut;
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;
        private IrcCommands _ircCommands;
        private bool _stopTask;

        public IRCClient()
        {
            _isConnectionEstablised = false;
            _IsClientRunning = false;
        }

        public void SetConnectionInformation(string ip, string username, string channels, int port = 0, string password = "", int timeout = 3000)
        {
            _newIp = ip;
            _NewPort = port;
            _NewUsername = username;
            _newPassword = password;
            _NewChannels = channels;
            _isConnectionEstablised = false;
            _IsClientRunning = false;
            _timeOut = timeout;
        }

        internal bool IsClientRunning()
        {
            return _IsClientRunning;
        }

        internal bool IsConnectionEstablished()
        {
            return _isConnectionEstablised;
        }

        public bool Connect()
        {
            try
            {
                _isConnectionEstablised = false;
                _receiverTask = new Task(BeginReceivingChat);
                _receiverTask.Start();

                int timeout = 0;
                while (!_isConnectionEstablised)
                {
                    Thread.Sleep(1);
                    if (timeout > _timeOut)
                    {
                        return false;
                    }
                    timeout++;
                }
                Trace.WriteLine("Connection succeeded.");

                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message + " " + e.Source + " " + e.StackTrace + " Connection has failed.");
                return false;
            }
        }

        internal bool StopClient()
        {
            _stopTask = true;
            //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs("CLOSING CLIENT", "CLOSE"));
            return QuitConnect();
        }

        private void BeginReceivingChat()
        {
            _tcpClient = new TcpClient(_newIp, _NewPort);

            int timeout = 0;
            while (!_tcpClient.Connected)
            {
                Thread.Sleep(1);

                if (timeout >= _timeOut)
                {
                    Trace.WriteLine("Timed out. Could not connect to socket.");

                }
                timeout++;
            }

            try
            {
                _networkStream = _tcpClient.GetStream(); Thread.Sleep(500);
                _streamReader = new StreamReader(_networkStream);
                _streamWriter = new StreamWriter(_networkStream);
                _ircCommands = new IrcCommands(_networkStream);

                _isConnectionEstablised = true;
                Trace.WriteLine("Connected to TCP socket.");

                if (_newPassword.Length > 0)
                {
                    if (!_ircCommands.SetPassword(_newPassword))
                    {
                        Trace.WriteLine("IRC Setup error.");

                        _isConnectionEstablised = false;
                    }
                }
                Debug.WriteLine("Joining channels: " + _NewChannels);
                if (!_ircCommands.JoinNetwork(_NewUsername, _NewChannels))
                {
                    Trace.WriteLine("IRC Setup error.");

                    _isConnectionEstablised = false;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message + " " + e.Source + " " + e.StackTrace + " IRC SETUP ERROR.");
            }



            if (_isConnectionEstablised)
            {
                Trace.WriteLine("Connected to IRC Server!");
                _stopTask = false;
                Task.Run(() => ReceiveChat());
            }
        }
        private void ReceiveChat()
        {
            try
            {
                Trace.WriteLine("Started listener.");


                Dictionary<string, List<string>> usersPerChannelDictionary = new Dictionary<string, List<string>>();

                _IsClientRunning = true;
                _isConnectionEstablised = true;
                while (!_stopTask)
                {

                    string ircData = _streamReader.ReadLine();

                    //OnRawMessageReceived?.Invoke(this, new IrcRawReceivedEventArgs(ircData));



                    if (ircData.Contains("PING"))
                    {
                        string pingID = ircData.Split(':')[1];
                        WriteIrc("PONG :" + pingID);
                    }
                    if (ircData.Contains("PRIVMSG"))
                    {

                        try
                        {
                            string messageAndChannel = ircData.Split(new string[] { "PRIVMSG" }, StringSplitOptions.None)[1];
                            string message = messageAndChannel.Split(':')[1].Trim();
                            string channel = messageAndChannel.Split(':')[0].Trim();
                            string user = ircData.Split(new string[] { "PRIVMSG" }, StringSplitOptions.None)[0].Split('!')[0].Substring(1);

                            channel = channel.Replace("=", string.Empty);

                            //OnMessageReceived?.Invoke(this, new IrcReceivedEventArgs(message, user, channel));
                        }
                        catch (Exception e)
                        {
                            //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs(ex.ToString(), "MESSAGE RECEIVED ERROR (PRIVMSG)"));
                        }



                    }
                    else if (ircData.Contains("JOIN"))
                    {


                        try
                        {
                            string channel = ircData.Split(new string[] { "JOIN" }, StringSplitOptions.None)[1].Split(':')[1];
                            string userThatJoined = ircData.Split(new string[] { "JOIN" }, StringSplitOptions.None)[0].Split(':')[1].Split('!')[0];

                            //OnMessageReceived?.Invoke(this, new IrcReceivedEventArgs("User Joined", userThatJoined, channel));
                        }
                        catch (Exception e)
                        {
                            //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs(ex.ToString(), "MESSAGE RECEIVED ERROR (JOIN)"));
                        }

                    }
                    else if (ircData.Contains("QUIT"))
                    {

                        //RAW: :MrRareie!~MrRareie_@Rizon-AC4B78B2.cm-3-2a.dynamic.ziggo.nl QUIT 
                        try
                        {
                            string user = ircData.Split(new string[] { "QUIT" }, StringSplitOptions.None)[0].Split('!')[0].Substring(1);

                            //OnMessageReceived?.Invoke(this, new IrcReceivedEventArgs("User Left", user, "unknown"));
                        }
                        catch (Exception e)
                        {
                            //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs(ex.ToString(), "MESSAGE RECEIVED ERROR (JOIN)"));
                        };
                    }

                    if (ircData.Contains(" 353 "))
                    {
                        try
                        {
                            string channel = ircData.Split(new[] { " " + _NewUsername + " =" }, StringSplitOptions.None)[1].Split(':')[0].Replace(" ", string.Empty);
                            string userListFullString = ircData.Split(new[] { " " + _NewUsername + " =" }, StringSplitOptions.None)[1].Split(':')[1];


                            if (!channel.Contains(_NewUsername) && !channel.Contains("="))
                            {
                                string[] users = userListFullString.Split(' ');
                                if (usersPerChannelDictionary.ContainsKey(channel))
                                {
                                    usersPerChannelDictionary.TryGetValue(channel, out var currentUsers);


                                    foreach (string name in users)
                                    {
                                        if (!name.Contains(_NewUsername))
                                        {
                                            currentUsers.Add(name);
                                        }
                                    }
                                    usersPerChannelDictionary[channel.Trim()] = currentUsers;
                                }
                                else
                                {
                                    List<string> currentUsers = new List<string>();
                                    foreach (string name in users)
                                    {
                                        currentUsers.Add(name);
                                    }
                                    usersPerChannelDictionary.Add(channel.Trim(), currentUsers);
                                }
                            }


                        }
                        catch (Exception e)
                        {
                            //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs(ex.ToString(), "MESSAGE RECEIVED ERROR (USERLIST)"));
                        }


                    }

                    if (ircData.ToLower().Contains(" 366 "))
                    {
                        //OnUserListReceived?.Invoke(this, new IrcUserListReceivedEventArgs(usersPerChannelDictionary));
                        usersPerChannelDictionary.Clear();
                    }
                    Thread.Sleep(1);
                }

                //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs("RECEIVER HAS STOPPED RUNNING", "MESSAGE RECEIVER"));

                QuitConnect();
                _stopTask = false;
            }
            catch (Exception e)
            {
                //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs("LOST CONNECTION: " + ioex.ToString(), "MESSAGE RECEIVER"));
                if (_isConnectionEstablised)
                {
                    _stopTask = false;
                    QuitConnect();
                }
            }
            _IsClientRunning = false;
        }
        public bool WriteIrc(string input)
        {
            try
            {
                _streamWriter.Write(input + Environment.NewLine);
                Trace.WriteLine("NEW INPUT: " + input + Environment.NewLine);
                _streamWriter.Flush();
                return true;
            }
            catch (Exception e)
            {
                //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs("Could not send message" + input + ", _tcpClient client is not running :X, error : " + e.ToString(), "MESSAGE SENDER")); ;
                return false;
            }

        }
        public bool QuitConnect()
        {
            //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs("STARTING IRC CLIENT SHUTDOWN PROCEDURE", "QUIT"));
            //send quit to server
            if (WriteIrc("QUIT"))
            {
                int timeout = 0;
                while (_tcpClient.Connected)
                {
                    Thread.Sleep(1);
                    if (timeout >= _timeOut)
                    {
                        return false;
                    }
                    timeout++;
                }

                _stopTask = true;
                Thread.Sleep(200);
                //stop everything in right order
                _receiverTask.Dispose();
                _streamReader.Dispose();
                _networkStream.Close();
                _streamWriter.Close();
                _tcpClient.Close();

                _isConnectionEstablised = false;

                //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs("FINISHED SHUTDOWN PROCEDURE", "QUIT"));
                return true;

            }
            else
            {
                //OnDebugMessage?.Invoke(this, new IrcDebugMessageEventArgs("COULD NOT WRITE QUIT COMMAND TO SERVER", "QUIT"));
                return true;
            }
        }
    }
}
