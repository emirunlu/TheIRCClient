using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IRCClient_Simple
{

    public class IRCTextListener : TraceListener
    {
        RichTextBox _box;
        string _data;
        public IRCTextListener(string initializeData)
        {
            _data = initializeData;
        }


        private bool Init()
        {
            if (_box != null && _box.IsDisposed)
            {
                _box = null;
            }
            if (_box == null)
            {
                foreach (Form f in Application.OpenForms)
                {
                    foreach (Control c in f.Controls)
                    {
                        if (c.Name == _data && c is RichTextBox)
                        {
                            _box = (RichTextBox)c;
                            break;
                        }
                    }
                }
            }
            return _box != null && !_box.IsDisposed;
        }

        public override void WriteLine(string message)
        {
            if (Init())
            {
                _box.Text = _box.Text + message + "\r\n";
            }
        }

        public override void Write(string message)
        {
            if (Init())
            {
                _box.Text = _box.Text + message;
            }
        }

    }
}