using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;


namespace ServerGame.Core.Permissions
{
    public class PermissionsUser : ServerGame.Interface.Permissions.IPermissionsUser
    {
        public bool CanAddNewUser { get; protected set; }

        public bool CanAddNewRoom { get; protected set; }

        public bool CanAddNewZoom { get; protected set; }

      
        public bool Loginby(string username, string password)
        {

            return true;
        }

        public bool Loginby(IUser user)
        {
            throw new NotImplementedException();
        }

        public bool Loginby(int id)
        {
            throw new NotImplementedException();
        }
    }
}
