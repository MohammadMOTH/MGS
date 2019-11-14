using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.EventStore;
using ServerGame.Interface.Event;
using ServerGame.Interface.Data;
using ServerGame.Core.Connctions;


namespace ServerGame.Core.EventStore
{

    public  class EventStoreAbstract : IEventStore
    {
        public static List<ServerGame.Interface.Event.IEvenets> StoredEvent { get; protected set; }
   

        public static void Parser( PackSendData packSendData , ref ServerGame.Core.Connctions.AbstractServer.StateObject DataUser ) {
            StoredEvent.Find(Event => string.Equals ( Event.NameEvent , packSendData.NameOFEvent))
                .OnRunEvent
                (
                packSendData
                ,ref DataUser
                ) ;
        
        }
        public  static  void AddNewEvent(ServerGame.Interface.Event.IEvenets Event) {
            if (StoredEvent == null)
                StoredEvent = new List<IEvenets>();

            StoredEvent.Add(Event);
        }
    }
}
