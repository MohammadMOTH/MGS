using ServerGame.Interface.Permissions;
using ServerGame.Interface.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Zone
{
    public abstract class ZoneAbstract : ServerGame.Interface.Zone.IZone
    {


        public ZoneAbstract (string ZoneName , int id , int UserMaxIdleTime , bool GuestUsersAccess
            , bool FilterUsernamesEnable
            , bool FilterRoomNamesEnable
            , bool FilterMessagesEnable
            , bool FileUploadEnable
            , int MaxUsers,
            int MaxRooms,
            
            bool IsActive
         )
        {

            this.ZoneName = ZoneName;
            this.Id = id;
            this.UserMaxIdleTime = UserMaxIdleTime;
            this.GuestUsersAccess = GuestUsersAccess;
            this.FilterUsernamesEnable = FilterUsernamesEnable;
            this.FilterRoomNamesEnable = FilterRoomNamesEnable;
            this.FilterMessagesEnable = FilterMessagesEnable;
            this.FileUploadEnable = FileUploadEnable;
            this.MaxUsers = MaxUsers;
            this.MaxRooms = MaxRooms;
         
              this.IsActive= IsActive;
            this.Rooms = new List<IRoom>();


        }
        public ZoneAbstract(string ZoneName, int id, int UserMaxIdleTime
     
        ,bool IsActive
      
        )
        {

            this.ZoneName = ZoneName;
            this.Id = id;
            this.UserMaxIdleTime = UserMaxIdleTime;
           
            this.PermissionsZone = PermissionsZone;
            this.IsActive = IsActive;
            this.Rooms = Rooms;
            this.GuestUsersAccess = false;
            this.FilterUsernamesEnable = false;
            this.FilterRoomNamesEnable = false;
            this.FilterMessagesEnable = false;
            this.FileUploadEnable = false;
            this.MaxRooms = 100;
            this.MaxUsers = MaxRooms * 400; 
 
        }

        public List<IPermissionsZone> PermissionsZone { get; protected set; }
        public bool IsActive { get; protected set; }
        public string ZoneName { get; protected set; }
        public List<IRoom> Rooms { get ; protected set ; }
        public int UserMaxIdleTime { get; protected set; }
        public bool GuestUsersAccess { get; protected set; }
        public bool FilterUsernamesEnable { get; protected set; }
        public bool FilterRoomNamesEnable { get; protected set; }
        public bool FilterMessagesEnable { get; protected set; }
        public bool FileUploadEnable { get; protected set; }
        public int MaxUsers { get; protected set; }
        public int MaxRooms { get; protected set; }
        public int Id { get; protected set; }
        public void ChangeName(string newname )
        {
            this.ZoneName = newname
                ;


        }
        public virtual void BroadcastingMessage(int zoneId, string message)
        {
            throw new NotImplementedException();
        }

        public virtual void ChangeStatusEnableDisable(bool state)
        {
            throw new NotImplementedException();
        }

        public virtual void FilterRoomNames()
        {
            throw new NotImplementedException();
        }

        public virtual  void FilterUsernames()
        {
            throw new NotImplementedException();
        }
    }
}
