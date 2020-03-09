using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.Event;
using ServerGame.Interface.User;
using ServerGame.Interface.Zone;

namespace ServerGame.Program.Room
{
   public class RoomLubby : Core.Room.Room
    {
        public RoomLubby(string Name, int Id, int MaxPlayers, DateTime DateStart, DateTime DataEnd, ref IZone IZone, ref IPermissionsRoom IPermissionsRoom)
        : base(Name, Id, MaxPlayers, DateStart, DataEnd, ref IZone, ref IPermissionsRoom)
        {


        }

    }
}
