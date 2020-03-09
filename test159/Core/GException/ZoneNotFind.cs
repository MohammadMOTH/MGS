using ServerGame.Interface.GException;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.GException
{
    public class ZoneNotFind : ServerGameExcptions
    {

        public ZoneNotFind() : base("number id Zone is incorrect ")
        {
            MessegsNumber = 2;

        }
    }
}
