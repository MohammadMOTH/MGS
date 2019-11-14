using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Event;
using ServerGame.Program.Event;
using ServerGame;
using ServerGame.Program.Data;
using ServerGame.Interface.Data;

namespace ServerGame.Core.Event
{

    public abstract class EventAbstract : IMessageSender
    {
  
        public string EventName { get ; set ; }


        public Message Data { get ; set  ; }
        ICustomeData Ievnet<object>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public abstract event EventHandler<MessageSenderEventArgs> MessageSending;
        public abstract event EventHandler<MessageSenderEventArgs> MessageSent;
        public event EventHandler<object> Send;

        public abstract void OnMessageSending(Message msg);
        public abstract void OnMessageSent(Message msg);

        public void OnSending(object sender, object arg)
        {
            throw new NotImplementedException();
        }
    }
}
