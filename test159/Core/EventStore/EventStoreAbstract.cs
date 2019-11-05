using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.EventStore;
using ServerGame.Interface.Event;
using ServerGame.Interface.Data;

namespace ServerGame.Core.EventStore
{
    public abstract class EventStoreAbstract : IEventStore
    {
        private static List<Ievnet> _storedEvent;
        public List<Ievnet> StoredEvent { get => _storedEvent; set => _storedEvent = value; }
        

        public abstract void Parser(ICustomeData data, IEnumerable<Type> list);
    }
}
