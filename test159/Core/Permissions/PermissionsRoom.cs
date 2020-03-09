using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Permissions
{
    public class PermissionsRoom : ServerGame.Interface.Permissions.IPermissionsRoom
    {
        public bool CanAddNewUser => throw new NotImplementedException();

        public bool CanAddNewRoom => throw new NotImplementedException();

        public bool CanAddNewZoom => throw new NotImplementedException();

        public bool Loginby(string username, string password)
        {
            throw new NotImplementedException();
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
