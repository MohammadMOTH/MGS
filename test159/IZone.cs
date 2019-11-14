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
   public interface IZone
    {

        /// <summary>
        /// ID Of Zone
        /// </summary>
         int Id { get; protected set; }
        /// <summary>
        /// Permissions If Zone
        /// </summary>
         List<IPermissionsZone> PermissionsZone { get; protected set; }
        /// <summary>
        /// Name Of Zone
        /// </summary>
         string ZoneName { get; protected set; }
        /// <summary>
        /// State Of Zone
        /// </summary>
         bool IsActive { get; protected set; }
        /// <summary>
        /// Max User Can Zone Have
        /// </summary>
         int MaxUsers { get; protected set; }
        /// <summary>
        /// Max Rooms Can Zone Have
        /// </summary>
         int MaxRooms { get; protected set; }
        /// <summary>
        /// List Of Rooms That Zone Have
        /// </summary>
         List<IRoom> Rooms { get; set; }
        /// <summary>
        /// Max IDLE Time (In Millisecond) For User
        /// </summary>
         int UserMaxIdleTime { get; set; }
        /// <summary>
        /// Permission To Allow Guest User Access To Zone
        /// </summary>
         bool GuestUsersAccess { get; set; }
        /// <summary>
        /// Enable Filtering Usernames
        /// </summary>
         bool FilterUsernamesEnable { get; set; }
        /// <summary>
        /// Enable Filtering Room Names
        /// </summary>
         bool FilterRoomNamesEnable { get; set; }
        /// <summary>
        /// Enable Filtering Messages
        /// </summary>
         bool FilterMessagesEnable { get; set; }

        /// <summary>
        /// Enable Uploading Files
        /// </summary>
         bool FileUploadEnable { get; set; }

        /// <summary>
        /// Change Status For Bool state 
        /// </summary>
        /// <param name="state"></param>
         void ChangeStatusEnableDisable(bool state); 

        /// <summary>
        /// Filtering Usernames
        /// </summary>
         void FilterUsernames();

        /// <summary>
        /// Filtering Room Names
        /// </summary>
         void FilterRoomNames();

        /// <summary>
        /// Broadcasting Message To All Zone 
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="message"></param>
         void BroadcastingMessage(int zoneId, string message);


    }
}
