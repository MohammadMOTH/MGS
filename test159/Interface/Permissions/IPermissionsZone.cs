using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Permissions
{
   public interface IPermissionsZone : IPermissions
    {
        bool CheckZoonCanLogin(string ZoonName, IUser User);
        bool CheckRoomCanLogin(string RoomName, IUser User);

    }
}
