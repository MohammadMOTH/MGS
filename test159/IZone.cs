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
    interface IZone
    {

        /// <summary>
        /// ID Of Zone
        /// </summary>
        public int Id { get; protected set; }
        /// <summary>
        /// Permissions If Zone
        /// </summary>
         List<IPermissionsZone> PermissionsZone { get; protected set; }
        /// <summary>
        /// Name Of Zone
        /// </summary>
        public string ZoneName { get; protected set; }
        /// <summary>
        /// State Of Zone
        /// </summary>
        public bool IsActive { get; protected set; }
        /// <summary>
        /// Max User Can Zone Have
        /// </summary>
        public int MaxUsers { get; protected set; }
        /// <summary>
        /// Max Rooms Can Zone Have
        /// </summary>
        public int MaxRooms { get; protected set; }
        /// <summary>
        /// List Of Rooms That Zone Have
        /// </summary>
        public List<IRoom> Rooms { get; set; }
        /// <summary>
        /// Max IDLE Time (In Millisecond) For User
        /// </summary>
        public int UserMaxIdleTime { get; set; }
        /// <summary>
        /// Permission To Allow Guest User Access To Zone
        /// </summary>
        public bool GuestUsersAccess { get; set; }
        /// <summary>
        /// Enable Filtering Usernames
        /// </summary>
        public bool FilterUsernamesEnable { get; set; }
        /// <summary>
        /// Enable Filtering Room Names
        /// </summary>
        public bool FilterRoomNamesEnable { get; set; }
        /// <summary>
        /// Enable Filtering Messages
        /// </summary>
        public bool FilterMessagesEnable { get; set; }

        /// <summary>
        /// Enable Uploading Files
        /// </summary>
        public bool FileUploadEnable { get; set; }

        /// <summary>
        /// Change Status For Bool state 
        /// </summary>
        /// <param name="state"></param>
        public void ChangeStatusEnableDisable(bool state); 

        /// <summary>
        /// Filtering Usernames
        /// </summary>
        public void FilterUsernames();

        /// <summary>
        /// Filtering Room Names
        /// </summary>
        public void FilterRoomNames();

        /// <summary>
        /// Broadcasting Message To All Zone 
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="message"></param>
        public void BroadcastingMessage(int zoneId, string message);


    }
}
