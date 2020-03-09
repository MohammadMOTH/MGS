using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Permissions
{
    public   interface IPermissions
    {


        bool Loginby(string username, string password);

        bool Loginby(Interface.User.IUser user);

        bool Loginby(int id);




    }
}
