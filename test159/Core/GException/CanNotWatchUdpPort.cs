using ServerGame.Interface.GException;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.GException
{
    public class CanNotWatchUdpPort : ServerGameExcptions
    {

        public CanNotWatchUdpPort() : base("i Can Not Watch Udp Port ")
        {
            MessegsNumber = 5;

        }
    }
}
