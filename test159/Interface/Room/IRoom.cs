using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.Event;

namespace ServerGame.Interface.Room
{
  public interface IRoom
    {
        #region Public_var

      

        /// <summary>
        /// Name of Room
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id of Room
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Max players can join in this room
        /// </summary>
        public int MaxPlayers { get; set; }


        /// <summary>
        ///  Permissions to inspict 
        /// </summary>
        public IPermissionsUser PermissionsUser { get; set; }


        /// <summary>
        /// list of user in this room
        /// </summary>
        public List<IUser> UserInRoom {  get;  set; }

        /// <summary>
        /// start time this room
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// time to end this room
        /// </summary>
        public DateTime DataEnd { get; set; }



        #endregion

        #region internal_var


        /// <summary>
        /// All evnets type and methods of envets
        /// </summary>
        List<Ievnet> _MyEvnets { get; set; }
        /// <summary>
        /// Zone Info
        /// </summary>
       public IZone IZone { get;  set; }

        #endregion

        #region Public_Method

        /// <summary>
        /// Add new event to Listener 
        /// </summary>
        /// <param name="evnet"></param>
        /// <returns></returns>
        public bool AddEventListener(Ievnet evnet);


        /// <summary>
        /// here we can add new user to my room and check all berfor
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool JoinUserToRoom(IUser User);

        /// <summary>
        /// here we can add new user to my room and check all berfor
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool Test();
        #endregion


        #region internal_Method



        #region CheckData
        /// <summary>
        /// Check If User on black list
        /// </summary>
        /// <param name="InputUserToSendData">user ,for check if is on black list </param>
        /// <returns>true is black , </returns>
        bool _CheckBlackList(IUser InputUserToSendData);

        /// <summary>
        /// Check If User on Permission list
        /// </summary>
        /// <param name="InputUserToSendData">user ,for check if is on Permission list </param>
        /// <returns>true blacked ,false not blacked </returns>
        bool _CheckPermissionsUser(IUser InputUserToSendData);

        /// <summary>
        /// Add New User To list
        /// </summary>
        /// <param name="User"></param>
        /// <returns>true added , false not added </returns>
        bool _AddNewUser(IUser User);
        #endregion


        #endregion

    }
}
