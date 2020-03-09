using ServerGame.Core.Connctions;
using ServerGame.Interface.Connctions;
using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Event
{
   public class ResEventArgs : EventArgs
    {
        public PackSendData PackSendData;
        public AbstractServer.StateObject UserSenderAllInfo;
        public ConnctionType ConnctionType;
     
        public ResEventArgs (PackSendData PackSendData , ref AbstractServer.StateObject UserSenderAllInfo , ConnctionType ConnctionType )
        {

              this.PackSendData= PackSendData;
            this.UserSenderAllInfo  =UserSenderAllInfo;
            this.ConnctionType = ConnctionType;
           
        }

    }
}
