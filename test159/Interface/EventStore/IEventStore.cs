using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Core.Connctions;
using ServerGame.Core.Event;
using ServerGame.Interface.Data;

namespace ServerGame.Interface.EventStore
{
  public interface IEventStore
    {
        /// <summary>
        /// List Of All Stored Events
        /// </summary>
        static  List<ServerGame.Core.Event.Event> StoredEvent { get; }


    }
}
