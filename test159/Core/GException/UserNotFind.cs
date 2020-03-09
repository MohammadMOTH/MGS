using ServerGame.Interface.GException;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.GException
{
    public class UserNotFind : ServerGameExcptions
    {

        public UserNotFind() : base("User Not find In list , you must adding on new list")
        {
            MessegsNumber = 4;

        }
    }
}
