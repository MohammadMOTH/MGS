using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Permissions
{
    interface IPermissionsZone : IPermissions
    {
        bool CheckZoonCanLogin(string ZoonName, IUser User);
        bool CheckRoomCanLogin(string RoomName, IUser User);

    }
}
