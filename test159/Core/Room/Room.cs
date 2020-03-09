using ServerGame.Interface.Permissions;
using ServerGame.Interface.Zone;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Room
{
  public  class Room : RoomAbstract
    {

       
        public Room(string Name, int Id, int MaxPlayers, DateTime DateStart, DateTime DataEnd, ref IZone IZone, ref IPermissionsRoom IPermissionsRoom)
        :base( Name,  Id,  MaxPlayers,  DateStart,  DataEnd, ref IZone,  ref IPermissionsRoom)
        {
            this.UserInRoom = new List<Interface.User.IUser>();

        }
    }
}
