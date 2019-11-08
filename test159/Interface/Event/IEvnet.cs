using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Data;

namespace ServerGame.Interface.Event
{
  public interface Ievnet<T>
    {
        #region Public_var
        /// <summary>
        /// Name of Event
        /// </summary>
        public string EventName { get; set; }

        public ICustomeData Data { get; set; }

        #endregion

        #region Public_Method

        /// <summary>
        /// This Event Raising When Sendign Your Data
        /// </summary>
        /// <param name="T">Put Your Data Type Which Will Sending With Event</param>
        public event EventHandler<T> Send;


        /// <summary>
        /// This Method Use For Raising The Event Before sending the message
        /// </summary>
        /// <param name="sender">Represent The Object Which Will be The Publisher That Will Sending Event Data</param>
        /// <param name="arg">Represent The Custome Type Of Data Which Will Be Sending Throwgh Event</param>
        public void OnSending(object sender, T arg);
        #endregion
    }
}
