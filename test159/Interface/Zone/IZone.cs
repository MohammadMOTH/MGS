using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.Room;

namespace ServerGame.Interface.Zone
{
    /// <summary>
    /// 
    /// </summary>
   public interface IZone
    {

        /// <summary>
        /// ID Of Zone
        /// </summary>
         int Id { get; }
        /// <summary>
        /// Permissions If Zone
        /// </summary>
         List<IPermissionsZone> PermissionsZone { get;}
        /// <summary>
        /// Name Of Zone
        /// </summary>
         string ZoneName { get;  }
        /// <summary>
        /// State Of Zone
        /// </summary>
         bool IsActive { get; }
        /// <summary>
        /// Max User Can Zone Have
        /// </summary>
         int MaxUsers { get;  }
        /// <summary>
        /// Max Rooms Can Zone Have
        /// </summary>
         int MaxRooms { get;  }
        /// <summary>
        /// List Of Rooms That Zone Have
        /// </summary>
         List<IRoom> Rooms { get; }
        /// <summary>
        /// Max IDLE Time (In Millisecond) For User
        /// </summary>
         int UserMaxIdleTime { get;  }
        /// <summary>
        /// Permission To Allow Guest User Access To Zone
        /// </summary>
         bool GuestUsersAccess { get;  }
        /// <summary>
        /// Enable Filtering Usernames
        /// </summary>
         bool FilterUsernamesEnable { get;  }
        /// <summary>
        /// Enable Filtering Room Names
        /// </summary>
         bool FilterRoomNamesEnable { get;  }
        /// <summary>
        /// Enable Filtering Messages
        /// </summary>
         bool FilterMessagesEnable { get;  }

        /// <summary>
        /// Enable Uploading Files
        /// </summary>
         bool FileUploadEnable { get;  }

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


        void ChangeName(string newname);

    }
}
