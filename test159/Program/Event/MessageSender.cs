using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ServerGame.Core.Event;
using ServerGame.Program.Data;

namespace ServerGame.Program.Event
{
    public class MessageSenderEventArgs : EventArgs
    {
        private Message _message;
        public Message Message { get => _message; set => _message = value; }

        public MessageSenderEventArgs(Message msg)
        {
            Message = msg;
        }
    }

    class MessageSender : MessageSenderAbstract
    {
        public MessageSender()
        {
            EventName = "MessageSender";
        }

        public override event EventHandler<MessageSenderEventArgs> MessageSending;
        public override event EventHandler<MessageSenderEventArgs> MessageSent;

        public override void OnMessageSending(Message msg)
        {
            
        }
        public override void OnMessageSent(Message msg)
        {
            Console.WriteLine("The Message Sending...");
            Thread.Sleep(1500);
            MessageSent?.Invoke(this, new MessageSenderEventArgs(msg));
        }


    }
}
