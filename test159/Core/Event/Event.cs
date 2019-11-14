using ServerGame.Core.Connctions;
using ServerGame.Core.Data;
using ServerGame.Interface.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Event
{

    [Serializable]
    public  class Event : IEvenets
    {
          public string  NameEvent { get; protected set; }
        

        public Event ()
        {
            this.NameEvent = "Chat";
        }

        public virtual bool OnRunEvent(PackSendData PackSendData, ref ServerGame.Core.Connctions.AbstractServer.StateObject UserSenderAllInfo)
        {
            Console.WriteLine(PackSendData.AllData[0].DataS);


            return true;
        }
    }
}
