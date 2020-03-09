using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Permissions
{
    public class PermissionsZone : Interface.Permissions.IPermissionsZone
    {

        public bool CanAddNewUser { get; set; }

        public bool CanAddNewRoom { get; set; }

        public bool CanAddNewZoom { get; set; }

        public bool CheckRoomCanLogin(string RoomName, IUser User)
        {
            throw new NotImplementedException();
        }

        public bool CheckZoonCanLogin(string ZoonName, IUser User)
        {
            throw new NotImplementedException();
        }

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
