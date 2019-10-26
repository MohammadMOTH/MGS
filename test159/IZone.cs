using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.Room;

namespace ServerGame
{
    /// <summary>
    /// 
    /// </summary>
  public  interface IZone
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public List<IPermissionsZone> PermissionsZone { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public string ZoneName { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaximumUsers { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaximumRooms { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public List<IRoom> Rooms { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserMaximumIdleTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool GuestUsersAccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool FilterUsernamesEnable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool FilterRoomNamesEnable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool FilterMessagesEnable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool FileUploadEnable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void ChangeStatusEnableDisable(bool state); 

        /// <summary>
        /// 
        /// </summary>
        public void FilterUsername();

        /// <summary>
        /// 
        /// </summary>
        public void FilterRoomNames();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="message"></param>
        public void BroadcastingMessage(int zoneId, string message);


    }
}
