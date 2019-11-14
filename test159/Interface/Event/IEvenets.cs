using ServerGame.Core.Connctions;
using ServerGame.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Event
{
    public interface IEvenets
    {
        string NameEvent { get; }


     bool OnRunEvent(PackSendData PackSendData, ref ServerGame.Core.Connctions.AbstractServer.StateObject UserSenderAllInfo);

    }
}
