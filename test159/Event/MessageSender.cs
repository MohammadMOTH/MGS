using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServerGame.Event
{
    public class MessageSenderEventArgs : EventArgs
    {
        public string message { get; set; }
    }
    class MessageSender
    {
        public event EventHandler<MessageSenderEventArgs> MessageSent;

        public void Send(string message)
        {
            Console.WriteLine("The Message Sending...");
            Thread.Sleep(3000);
            OnMessageSent(message);
        }

        public virtual void OnMessageSent(string message)
        {
            if (MessageSent != null)
            {
                MessageSent(this, new MessageSenderEventArgs() { message = message });
            }
        }
    }
}
