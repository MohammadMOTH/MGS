using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ServerGame.Core.Connctions;
using ServerGame.Core.GException;
using ServerGame.Core.Permissions;
using ServerGame.Core.User;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.User;
using ServerGame.Interface.Zone;
namespace ServerGame.Program.Zone
{
  public  class ZoneMaster
    {
        #region GetZone
        public static   IZone GetZoneBy(int Id)
        {
       var zoen =    Server.ZoneWorking.Find(x => x.Id == Id);
            if (zoen == null)
                throw  new ZoneNotFind();
            return  zoen;
        }
        public static IZone GetZoneBy(string ZoneName)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Send_Meesges
        public static bool SendMessgeToAllUserInZoneBy(int Id , ServerGame.Core.Connctions.PackSendData PackSendData)
        {
            throw new NotImplementedException();
        }
        public static bool SendMessgeToAllUserInZoneBy(string ZoneName, ServerGame.Core.Connctions.PackSendData PackSendData)
        {
            throw new NotImplementedException();
        }
        public static bool SendMessgeToAllUserInZoneBy(IZone IZone, ServerGame.Core.Connctions.PackSendData PackSendData)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CanLogin
        public static bool CanLoginToZoneBy(string username , string password , IZone IZone)
        {
           
            if (IZone.IsActive && IZone.GuestUsersAccess)
                return true;
            if (IZone.PermissionsZone[0].Loginby(username, password))
                return true;
            return false;

        }
        public static bool CanLoginToZoneBy(string username, string password, int Id)
        {
            var zonebyid = GetZoneBy(Id);
            if (zonebyid.IsActive && zonebyid.GuestUsersAccess)
                return true;
            if ( zonebyid.PermissionsZone[0].Loginby( username, password ) )
                return true;

            throw new UserNameOrPasswordError();
        }

        public static bool CanLoginToZoneBy(string username, string password, string ZoneName)
        {
            throw new NotImplementedException();
        }

      
        #endregion

        #region AddRoom
        public static bool AddRoomBy(string RoomClassType,ref IZone IZone , string Name, int Id, int MaxPlayers, DateTime DateStart, DateTime DataEnd)
        {
          

            var objectx = Type.GetType("ServerGame.Program.Room." + RoomClassType);
            object[] paramArray = { RoomClassType, Id, MaxPlayers, DateStart, DataEnd, IZone, new ServerGame.Core.Permissions.PermissionsRoom() };
            IZone.Rooms.Add ((Interface.Room.IRoom)Activator.CreateInstance(objectx, paramArray));
          
            return true;
        }
        public static bool AddRoomBy(string username, string password, int Id)
        {
            var zonebyid = GetZoneBy(Id);
            if (zonebyid.IsActive && zonebyid.GuestUsersAccess)
                return true;
            if (zonebyid.PermissionsZone[0].Loginby(username, password))
                return true;

            throw new UserNameOrPasswordError();
        }

        public static bool AddRoomBy(string username, string password, string ZoneName)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Add User To Room Lubby
        public static bool AddUserToRoomLubbyBy(ref ServerGame.Interface.User.IUser user , int idroom , ref IZone IZone)
        {
        var room =     IZone.Rooms.Find(x => x.Id == idroom);
            if (room == null)
            {
             
               
                object[] paramArray = { "RoomLubby", -1, 400, DateTime.Now, new DateTime(DateTime.Now.AddHours(12f).Ticks), IZone, new ServerGame.Core.Permissions.PermissionsRoom() };
                room = (Interface.Room.IRoom)Activator.CreateInstance(GetTypeRoomBy(idroom), paramArray);
                IZone.Rooms.Add(room);
                room.AddNewUser( user);
            }



             

            return true;
        }
        public static int IdInttempUDP (EndPoint IPEndPointUDP)
        {
         return   BitConverter.ToInt32((IPEndPointUDP as IPEndPoint).Address.GetAddressBytes(), 0) + (IPEndPointUDP as IPEndPoint).Port;

        }
        public static bool AddUserToRoomLubbyBy(string username , string password, int idroom, ref IZone IZone , AbstractServer.StateObject Connction ,out IUser user)
        {
            var room = IZone.Rooms.Find(x => x.Id == idroom);
            if (room == null)
            {


                object[] paramArray = { "RoomLubby", -1, 400, DateTime.Now, new DateTime(DateTime.Now.AddHours(12f).Ticks), IZone, new ServerGame.Core.Permissions.PermissionsRoom() };
                room = (Interface.Room.IRoom)Activator.CreateInstance(GetTypeRoomBy(idroom), paramArray);
                IZone.Rooms.Add(room);
               
            }
            var IdIntTempUDP = -1; 
           if (Connction.IPEndPointUDP !=null)
            IdIntTempUDP = IdInttempUDP(Connction.IPEndPointUDP);


            try
            {
                user =  UserMaster.ReturnUserBy( username);
            }
            catch (Core.GException.UserNotFind )
            {
             
                user =  UserMaster.AddNewUser(username, -1, Connction, ref room, IdIntTempUDP);
            }
             
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                user = null;
            }

            if (user == null)
                return false;

           if (null==room.UserInRoom.Find(x => x.Name == username ))
            room.AddNewUser(user);
       
            return true;
        }
        #endregion



        #region Get type Room By Id

        public static Type GetTypeRoomBy (int idroom)
        {
           var type =  ServerGame.Program.Room.RoomMaster.AllRooms.Find(x => x.id == idroom);

            if (type == null)
                throw new ServerGame.Core.GException.ZoneNotFind();


            return type.room;
        }


        #endregion

    }




}
