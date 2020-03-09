using ServerGame.Core.Connctions;
using ServerGame.Core.Data;
using ServerGame.Interface.Connctions;
using ServerGame.Interface.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Event
{

    [Serializable]
    public  class LoopSendingUDP : IEvenets
    {
        public Action<object, ResEventArgs> onevent { get; private set; }

        public ushort NameEvent { get; protected set; }

        public LoopSendingUDP(Action<object, ResEventArgs> onevent)
        {
            this.onevent = onevent;
            NameEvent = 0;
        }

        public virtual bool OnRunEvent(PackSendData PackSendData, ref ServerGame.Core.Connctions.AbstractServer.StateObject UserSenderAllInfo , ConnctionType ConnctionType)
        {

            if (this.onevent != null)
            {
                this.onevent(this, new ResEventArgs(PackSendData, ref UserSenderAllInfo , ConnctionType));
                return true;
            }
            else
            {
                for (int i = 0; i < PackSendData.AllData.Count; i++)
                {
                    Connctions.Server.ServerOject.Sendudp(UserSenderAllInfo.workSocket, PackSendData.AllData[i].DataS.ToString(), UserSenderAllInfo.IPEndPointUDP);
                    #if (DEBUG)
                    Console.WriteLine("Data Out Put Data[{0}] : {1} ", i.ToString(), PackSendData.AllData[i].DataS.ToString());
                    #endif
                }
                return true;
            }
            return true;
        }
    }
}
