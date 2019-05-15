using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace IRCClient_Simple
{
    internal class IrcCommands
    {
        private StreamWriter _writer;
        private StreamReader _reader;
        private int _responseNumber;
        private string _errorMessage;
        private string _username;
        private string _channels;

        public IrcCommands(NetworkStream networkStream)
        {
            _reader = new StreamReader(networkStream);
            _writer = new StreamWriter(networkStream);
        }

        public bool SetPassword(string password)
        {
            _writer.WriteLine("PASS " + password + Environment.NewLine);
            _writer.Flush();

            while (true)
            {
                string ircData = _reader.ReadLine();
                if (ircData.Contains("462"))
                {
                    _responseNumber = 462;
                    _errorMessage = "PASSWORD ALREADY REGISTERED";
                    return false;
                }
                else if (ircData.Contains("461"))
                {
                    _responseNumber = 461;
                    _errorMessage = "PASSWORD COMMAND NEEDS MORE PARAMETERS";
                    return false;
                }
                else if (ircData.Contains("004"))
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool JoinNetwork(string user, string channels)
        {
            _username = user;
            _channels = channels;
            string newLine = Environment.NewLine;
            _writer.WriteLine("NICK " + user + newLine);
            Trace.WriteLine("NICK " + user + newLine);
            _writer.Flush();
            _writer.WriteLine("USER " + user + " 8 * :" + user + "_TheIRCClient" + newLine);
            Trace.WriteLine("USER " + user + " 8 * :" + user + "_TheIRCClient" + newLine);
            _writer.Flush();

            while (true)
            {
                try
                {
                    string ircData = _reader.ReadLine();
                    if (ircData != null)
                    {
                        if (ircData.Contains("PING"))
                        {
                            string pingID = ircData.Split(':')[1];
                            _writer.WriteLine("PONG :" + pingID);
                            _writer.Flush();
                        }

                        if (CheckMessageForError(ircData))
                        {
                            if (_responseNumber == 266)
                            {
                                return JoinChannel(channels, user);
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
                catch (Exception e)
                {

                    Trace.WriteLine("RECEIVED: " + e.ToString());
                    return false;
                }
            }
        }
        public bool JoinChannel(string channels, string username)
        {
            _channels = channels;
            Trace.WriteLine("Connecting to channel " + channels);
            _writer.WriteLine("JOIN " + channels + Environment.NewLine);
            _writer.Flush();
            while (true)
            {

                string ircData = _reader.ReadLine();
                Trace.WriteLine("IRCDATA: " + ircData);
                if (ircData != null)
                {
                    if (ircData.Contains("PING"))
                    {
                        string pingID = ircData.Split(':')[1];
                        _writer.WriteLine("PONG :" + pingID);
                        _writer.Flush();
                    }

                    if (ircData.Contains(username) && ircData.Contains("JOIN"))
                    {
                        return true;
                    }
                }
                Thread.Sleep(1);
            }
        }
        public bool CheckMessageForError(string message)
        {
            string codeString = message.Split(' ')[1].Trim();
            Trace.WriteLine("Codestring: " + codeString);
            if (int.TryParse(codeString, out _responseNumber))
            {
                foreach (string errorMessage in RFCCodes.ErrorList)
                {
                    if (errorMessage.Contains(codeString))
                    {
                        _errorMessage = errorMessage;
                        return false;
                    }
                }

                _errorMessage = "Message does not contain Error Code!";
                return true;
            }
            else
            {
                Debug.WriteLine("Could not parse number");
                _responseNumber = 0;
                _errorMessage = "Message does not contain Error Code, could not parse number!";
                return true;
            }


        }
    }
}