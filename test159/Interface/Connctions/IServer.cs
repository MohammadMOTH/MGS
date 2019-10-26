using System;
using System.Collections.Generic;
using System.Text;
using ServerGame;
namespace ServerGame.Interface.Connctions
{

    /// <summary>
    /// Interface of server
    /// </summary>
    /// <remarks>
    /// Interface of server . we mange here all of commnction to Clint .
    /// it've tpc and udp ports . send data asyn tcp and upd .
    /// list of clint udp and tcp . and it ref to user to porwer full manger connctions
    /// </remarks>
    interface IServer
    {
        #region Public_var
        public int UDPPort { get; protected set; }
        public int TCPPort { get; protected set; }
        /// <summary>
        /// type of protocol
        /// </summary>
        /// <remarks></remarks>
        /// <value>type of protocol</value>


        #endregion

        #region internal_var


        #endregion

        #region Public_Method

        void TcpSendData(IData Data, IUser User);
        void UdpSendData(IData Data, IUser User);
        #endregion


        #region internal_Method

        void _SendData(IData Data, IUser User , ConnctionType ConnctionType);

        #endregion
    }




}
