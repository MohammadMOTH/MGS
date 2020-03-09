using ServerGame.Core.Connctions;
using ServerGame.Core.Data;
using ServerGame.Core.Event;
using ServerGame.Interface.Connctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Event
{
    public interface IEvenets
    {
        ushort NameEvent { get; }
        Action<object, ResEventArgs> onevent { get; }

     bool OnRunEvent(PackSendData PackSendData, ref ServerGame.Core.Connctions.AbstractServer.StateObject UserSenderAllInfo , ConnctionType ConnctionType );

    }
}
