using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.GException
{
    public abstract class ServerGameExcptions : Exception
    {
        public int MessegsNumber;
        public ServerGameExcptions(string messge = "Error !") : base(messge)
        {

        }
    }

}
