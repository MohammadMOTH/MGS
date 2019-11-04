using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Data;

namespace ServerGame.Interface.Event
{
  public interface Ievnet
    {
        #region Public_var
        /// <summary>
        /// Name of Event
        /// </summary>
        public string EventName { get; set; }

        #endregion


        #region Suggestion
        // Mr.Mohammad Please Note The Bottom Code Is Just Suggestion So If It's Not Important You Can Delete It, Thanks
        // Mr.Mohammad Please Note The Bottom Code Is Just Suggestion So If It's Not Important You Can Delete It, Thanks
        // Mr.Mohammad Please Note The Bottom Code Is Just Suggestion So If It's Not Important You Can Delete It, Thanks


        /// <summary>
        /// This Event Raising Before sending the message
        /// </summary>
        //public event EventHandler<T> BeforeRaising;

        /// <summary>
        /// This Event Raising After sending the message
        /// </summary>
        //public event EventHandler<T> After;

        /// <summary>
        /// This Method Use For Raising The Event Before sending the message
        /// </summary>
        //public void OnBefore(object o);

        /// <summary>
        /// This Method Use For Raising The Event After sending the message
        /// </summary>
        //public void OnAfter(object o);

        #endregion
    }
}
