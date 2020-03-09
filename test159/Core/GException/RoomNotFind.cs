using ServerGame.Interface.GException;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.GException
{
    public class RoomNotFind : ServerGameExcptions
    {

        public RoomNotFind() : base("number id Zone is incorrect ")
        {
            MessegsNumber = 3;

        }
    }
}
