using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Program.Event;
using ServerGame.Program.Data;
using ServerGame.Interface.Data;

namespace ServerGame.Interface.Event
{
    public interface IMessageSender : Ievnet,IEventData<Message>
    {
        #region Public_event
        /// <summary>
        /// This Event Raising Before sending the message
        /// </summary>
        public event EventHandler<MessageSenderEventArgs> MessageSending;

        /// <summary>
        /// This Event Raising After sending the message
        /// </summary>
        public event EventHandler<MessageSenderEventArgs> MessageSent;

        #endregion

        #region Public_Method
        /// <summary>
        /// This Method Use For Raising The Event Before sending the message
        /// </summary>
        /// <param name="msg">The Message Which Will Send</param>
        public void OnMessageSending(Message msg);

        /// <summary>
        /// This Method Use For Raising The Event After sending the message
        /// </summary>
        /// /// <param name="msg">The Message Which Will Send</param>
        public void OnMessageSent(Message msg);

        #endregion
    }
}
