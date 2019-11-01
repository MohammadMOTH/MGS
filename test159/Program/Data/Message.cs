using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Program.Data
{
    public class Message:IData
    {
        private string _message;

        public string message { get => _message; set => _message = value; }

        public Message(string msg)
        {
            this._message = msg;
        }
    }
}
