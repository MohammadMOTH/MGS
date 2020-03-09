using ServerGame.Interface.Room;
using ServerGame.Interface.User;
using ServerGame.Interface.Zone;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Program.Room
{
  public  class RoomMaster
    {
        public static List<IdRooms> AllRooms = new List<IdRooms>();


        public static void init ()
        {

            initListRoomsById();

        }


        public static void initListRoomsById ()
        {
            AllRooms.Add(new IdRooms(-1, Type.GetType("ServerGame.Program.Room.RoomLubby")));
                

        }






        public static void BradCastToAllOfData(ref IZone zone , Core.Connctions.PackSendData PackSendData, int room, bool sendtoself, IUser userSender)
        {

          var roomX =   zone.Rooms.Find(x => x.Id == room);
            roomX.SendToAllUsers(PackSendData , Interface.Connctions.ConnctionType.UDP,  sendtoself,  userSender);

        }

        public static void BradCastToAllOfData( IRoom roomX, Core.Connctions.PackSendData PackSendData, bool sendtoself, IUser userSender , Interface.Connctions.ConnctionType ConnctionType)
        {

            
            roomX.SendToAllUsers(PackSendData, ConnctionType,  sendtoself,  userSender);

        }









    }

    public class IdRooms
    {
        public Type room;
        public int id;
        public IdRooms (int id , Type room)
        {
            this.room = room;
            this.id = id;

        }
    }
}
