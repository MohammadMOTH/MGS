using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ServerGame.Core.Event;
using ServerGame.Program.Data;

namespace ServerGame.Event
{
    public class MessageSenderEventArgs : EventArgs
    {
        public Message message { get; set; }
    }
    class MessageSender : MessageSenderAbstract
    {
        public MessageSender()
        {
            EventName = "MessageSender";
        }
        //public override string EventName { get; set; }

        public override event EventHandler<MessageSenderEventArgs> MessageSending;
        public override event EventHandler<MessageSenderEventArgs> MessageSent;

        public override void OnMessageSending(Message msg)
        {
            
        }
        public override void OnMessageSent(Message msg)
        {
            Console.WriteLine("The Message Sending...");
            Thread.Sleep(3000);
            MessageSent?.Invoke(this, new MessageSenderEventArgs() { message = msg });
        }


    }
}
