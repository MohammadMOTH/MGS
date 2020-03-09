using ServerGame.Interface.GException;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.GException
{
    public class UserNameOrPasswordError : ServerGameExcptions
    {

        public UserNameOrPasswordError() : base("Username or password is incorrect ")
        {
            MessegsNumber = 1;

        }
    }
}
