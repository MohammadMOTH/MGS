using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.Event;
using ServerGame.Interface.User;
using ServerGame.Interface.Zone;

namespace ServerGame.Interface.Room
{
  public interface IRoom
    {
        #region _var

 

        /// <summary>
        /// Name of Room
        /// </summary>
        string Name { get;  }
        /// <summary>
        /// Id of Room
        /// </summary>
         int Id { get;  }

        /// <summary>
        /// Max players can join in this room
        /// </summary>
         int MaxPlayers { get;  }


        /// <summary>
        ///  Permissions to inspict 
        /// </summary>
      ref  IPermissionsRoom IPermissionsRoom { get;  }


        /// <summary>
        /// list of user in this room
        /// </summary>
         List<IUser> UserInRoom {  get;   }

        /// <summary>
        /// start time this room
        /// </summary>
         DateTime DateStart { get;  }

        /// <summary>
        /// time to end this room
        /// </summary>
         DateTime DataEnd { get;  }



        #endregion

        #region internal_var
       ref IZone Zone { get; }


        #endregion

        #region Method



        /// <summary>
        /// here we can add new user to my room and check all berfor
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        bool JoinUserToRoom( IUser User);

        /// <summary>
        /// here we can add new user to my room and check all berfor
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        bool SendToAllUsers(ServerGame.Core.Connctions.PackSendData PackSendData, Interface.Connctions.ConnctionType ConnctionType, bool sendtoself, IUser userSender);

        bool SendToUsers( ServerGame.Core.Connctions.PackSendData PackSendData ,List<IUser> ListUsers, Interface.Connctions.ConnctionType ConnctionType);

        bool SendToUser(ServerGame.Core.Connctions.PackSendData PackSendData,IUser User, Interface.Connctions.ConnctionType ConnctionType);

        bool SendToAllZone(ServerGame.Core.Connctions.PackSendData PackSendData, Interface.Connctions.ConnctionType ConnctionType);

        bool SendToGroupUser(ServerGame.Core.Connctions.PackSendData PackSendData, IGroup group, Interface.Connctions.ConnctionType ConnctionType);

        /// <summary>
        /// Check If User on black list
        /// </summary>
        /// <param name="InputUserToSendData">user ,for check if is on black list </param>
        /// <returns>true is black , </returns>
        bool CheckBlackList(IUser InputUserToSendData);

        /// <summary>
        /// Check If User on Permission list
        /// </summary>
        /// <param name="InputUserToSendData">user ,for check if is on Permission list </param>
        /// <returns>true blacked ,false not blacked </returns>
         bool CheckPermissionsUser(IUser InputUserToSendData);

        /// <summary>
        /// Add New User To list
        /// </summary>
        /// <param name="User"></param>
        /// <returns>true added , false not added </returns>
         bool AddNewUser( IUser User);

        void ChangeName(string newname);



        #endregion

    }
}
